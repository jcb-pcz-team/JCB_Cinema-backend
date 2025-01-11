using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for handling authentication actions such as registration, login, and password management.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationsController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationsController"/> class.
        /// </summary>
        /// <param name="configuration">The application's configuration settings.</param>
        /// <param name="logger">The logger for capturing logs related to authentication activities.</param>
        /// <param name="userManager">The UserManager for managing application users.</param>
        /// <param name="userRoleService">Service to handle user role assignments.</param>
        /// <param name="userService">Service to handle user authentication and registration.</param>
        public AuthenticationsController(IConfiguration configuration, ILogger<AuthenticationsController> logger, UserManager<AppUser> userManager, IUserRoleService userRoleService, IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _userRoleService = userRoleService;
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="model">A <see cref="RegistrationModel"/> object containing the user's registration details.</param>
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
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            var result = await _userService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Register), new { userName = model.UserName }, model);
            }

            // Return errors as 400 Bad Request
            return BadRequest(result.Errors.Select(e => e.Description));
        }

        /// <summary>
        /// Logs in a user and generates a JWT token and refresh token for authentication.
        /// </summary>
        /// <param name="model">A <see cref="LoginModel"/> object containing the user's login credentials (username and password).</param>
        /// <returns>
        ///   * Status200OK (with data): If the login is successful, the method returns a 200 OK response with a <see cref="LoginResponse"/> object containing the JWT token, its expiration date, and the refresh token.
        ///   * Status401Unauthorized (no data): If the username or password is incorrect, the method returns a 401 Unauthorized response.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs during login, the method returns a 500 Internal Server Error response with a generic error message. Consider providing more specific error details in a production environment. 
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Call the service method to perform the login
                var token = await _userService.Login(model);

                // Return the JWT token if login is successful
                _logger.LogInformation("Login succeeded for user: {UserName}", model.UserName ?? model.Email);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log unauthorized access attempt
                _logger.LogWarning("Login failed for user: {UserName}. Reason: {Message}", model.UserName ?? model.Email, ex.Message);
                return Unauthorized(new { message = "Invalid username, email, or password" });
            }
            catch (Exception ex)
            {
                // Log any other exceptions that might occur
                _logger.LogError(ex, "An error occurred during login for user: {UserName}", model.UserName ?? model.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred. Please try again later." });
            }
        }

        /// <summary>
        /// Assigns a user to a specified role.
        /// </summary>
        /// <param name="roleRequest">A <see cref="AssignUserToRoleRequest"/> object containing the user and role to be assigned.</param>
        /// <returns>
        ///   * Status200OK (with data): If the role is successfully assigned, the method returns a 200 OK response with the assigned role details.
        ///   * Status400BadRequest (with message): If there is an error while assigning the role, the method returns a 400 Bad Request response.
        ///   * Status500InternalServerError (no data): If an unexpected error occurs, the method returns a 500 Internal Server Error response.
        /// </returns>
        [HttpPost("assign-user-to-role")]
        public async Task<IActionResult> AssignUserToRole([FromQuery] AssignUserToRoleRequest roleRequest)
        {
            try
            {
                var token = await _userService.AssignUserToRole(roleRequest);
                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning role to user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while assigning role.");
            }
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="user">A <see cref="ChangeUserPassword"/> object containing the user's current and new passwords.</param>
        /// <returns>
        ///   * Status200OK: If the password change is successful, the method returns a 200 OK response.
        ///   * Status401Unauthorized: If the username or password is incorrect, the method returns a 401 Unauthorized response.
        ///   * Status500InternalServerError: If an unexpected error occurs during password change, the method returns a 500 Internal Server Error response.
        /// </returns>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromQuery] ChangeUserPassword user)
        {
            try
            {
                await _userService.ChangePassword(user);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning role to user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while assigning role.");
            }
        }
    }
}
