using JCB_Cinema.Application.Auth;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JCB_Cinema.WebAPI.Controllers
{
    [ApiController]
    [Route("api/authentications")]
    public class AuthenticationsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationsController> _logger;

        public AuthenticationsController(UserManager<AppUser> userManager, IConfiguration configuration, ILogger<AuthenticationsController> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="model">A RegistrationModel object containing the user's registration details.</param>
        /// <remarks>
        /// Sample request with admin claim:
        /// <code>
        ///     POST api/authentications/register
        ///     {
        ///         "userName": "admin",
        ///         "password": "Ad123!",
        ///         "email": "user@example.com",
        ///         "role": "admin"
        ///     }
        /// </code>
        /// </remarks>
        /// <returns>
        ///   * Status201Created (no data): If the user is successfully registered, the method returns a 201 Created response with no content in the body.
        ///   * Status409Conflict (no data): If a user with the same username already exists, the method returns a 409 Conflict response with a message indicating the conflict.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs during registration, the method returns a 500 Internal Server Error response with a generic error message. Consider providing more specific error details in a production environment.
        /// </returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            _logger.LogInformation("Start Register");
            var existing = await _userManager.FindByNameAsync(model.UserName);
            if (existing != null)
            {
                return Conflict("User already exists");
            }
            var newUser = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Role = model.Role,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Register succeeded");

                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error: {result.Errors.Select(e => e.Description)}");
        }

        /// <summary>
        /// Logs in a user and generates a JWT token and refresh token for authentication.
        /// </summary>
        /// <param name="model">A LoginModel object containing the user's login credentials (username and password).</param>
        /// <returns>
        ///   * Status200OK (with data): If the login is successful, the method returns a 200 OK response with a LoginResponse object containing the JWT token, its expiration date, and the refresh token.
        ///   * Status401Unauthorized (no data): If the username or password is incorrect, the method returns a 401 Unauthorized response.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs during login, the method returns a 500 Internal Server Error response with a generic error message. Consider providing more specific error details in a production environment. 
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Start Login");

            AppUser? user = null;
            if (!string.IsNullOrEmpty(model.UserName))
            {
                user = await _userManager.FindByNameAsync(model.UserName);
            }
            else if (!string.IsNullOrEmpty(model.Email))
            {
                user = await _userManager.FindByEmailAsync(model.Email);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }
            JwtSecurityToken token = GenerateJwt(user.UserName!, user.Role);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["JWTExtraSettings:RefreshTokenExpiryMinutes"] ?? 5.ToString())
                    );

            await _userManager.UpdateAsync(user);

            _logger.LogInformation("Login succeeded");
            return Ok(new LoginResponse
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Refreshes a JWT token using a previously issued refresh token.
        /// </summary>
        /// <param name="model">A RefreshModel object containing the expired access token and the refresh token.</param>
        /// <returns>
        ///   * Status200OK (with data): If the refresh token is valid and not expired, the method returns a 200 OK response with a new LoginResponse object containing a new JWT token, its expiration date, and the same refresh token (optional based on implementation).
        ///   * Status401Unauthorized (no data): If the access token is invalid, the refresh token is invalid or expired, or the user is not found, the method returns a 401 Unauthorized response.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs during refresh, the method returns a 500 Internal Server Error response with a generic error message. Consider providing more specific error details in a production environment.
        /// </returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            _logger.LogInformation($"Start Refresh");
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal?.Identity?.Name is null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var token = GenerateJwt(principal.Identity.Name, user.Role);

            _logger.LogInformation($"Refresh succeeded");
            return Ok(new LoginResponse
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo,
                RefreshToken = model.RefreshToken
            });
        }

        /// <summary>
        /// Revokes the refresh token associated with the currently authorized user.
        /// </summary>
        /// <remarks>
        /// This method requires authorization (e.g., valid access token) and only allows a user to revoke their own refresh token.
        /// </remarks>
        /// <returns>
        ///   * Status200OK (no data): If the refresh token is successfully revoked (set to null), the method returns a 200 OK response with no content in the body.
        ///   * Status401Unauthorized (no data): If the user is not authorized or cannot be found, the method returns a 401 Unauthorized response.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs during revocation, the method returns a 500 Internal Server Error response with a generic error message. Consider providing more specific error details in a production environment.
        /// </returns>
        [Authorize]
        [HttpDelete("revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Revoke()
        {
            _logger.LogInformation("Start Revoke");

            var username = HttpContext.User.Identity?.Name;

            if (username is null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return Unauthorized();
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);
            _logger.LogInformation("Revoke succeeded");
            return Ok();
        }


        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var secret = _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

            var validation = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private JwtSecurityToken GenerateJwt(string username, string? role = null)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (role != null && IdentityData.AdminUserClaimName.Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                authClaims.Add(new Claim("admin", true.ToString(), ClaimValueTypes.Boolean));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                 issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddSeconds(
                    double.Parse(_configuration["JWTExtraSettings:TokenExpirySeconds"] ?? 45.ToString())
                    ),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
