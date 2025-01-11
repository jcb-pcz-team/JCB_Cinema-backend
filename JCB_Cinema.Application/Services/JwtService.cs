using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class for handling JWT token generation and validation. Provides methods to generate, write, and validate JWT tokens for users.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="configuration">Configuration instance for reading JWT settings.</param>
        /// <param name="userManager">UserManager instance for managing user roles.</param>
        public JwtService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Generates a JWT token for the given user and their roles.
        /// </summary>
        /// <param name="user">AppUser object representing the user.</param>
        /// <returns>A <see cref="JwtSecurityToken"/> containing the user's claims and roles.</returns>
        /// <exception cref="Exception">Throws if the user is null or invalid.</exception>
        public async Task<JwtSecurityToken> GenerateJwtAsync(AppUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName))
            {
                throw new Exception("User is not valid");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Fetch user roles and add them as claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddSeconds(
                    double.Parse(_configuration["JWTExtraSettings:TokenExpirySeconds"] ?? "4500000")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        /// <summary>
        /// Converts the generated JWT token into a string representation.
        /// </summary>
        /// <param name="token">JwtSecurityToken object.</param>
        /// <returns>A string representation of the JWT token.</returns>
        public string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates the JWT token and returns the ClaimsPrincipal if valid.
        /// </summary>
        /// <param name="token">JWT token string.</param>
        /// <returns>A <see cref="ClaimsPrincipal"/> object if the token is valid, otherwise null.</returns>
        public ClaimsPrincipal? ValidateJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured"));

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ClockSkew = TimeSpan.FromSeconds(5),
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
