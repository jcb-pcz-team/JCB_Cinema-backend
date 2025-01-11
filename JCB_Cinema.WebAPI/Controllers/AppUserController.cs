using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing application user data.
    /// Provides endpoints for retrieving, updating, and changing user information.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserController"/> class.
        /// </summary>
        /// <param name="appUserService">The service for interacting with app user data.</param>
        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        /// <summary>
        /// Retrieves user details based on query parameters.
        /// </summary>
        /// <param name="request">The query request to filter user data.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation. 
        /// If found, returns user details; otherwise, returns NotFound.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryAppUser request)
        {
            try
            {
                var req = await _appUserService.GetAppUserAsync(request);
                return req == null ? NotFound() : Ok(req);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <param name="reqUser">The user details to be updated.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the outcome of the update operation.</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutAppUserDetails reqUser)
        {
            try
            {
                await _appUserService.PutAppUserAsync(reqUser);
                return Created();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Updates the user's email address.
        /// </summary>
        /// <param name="reqUserEmail">The request containing the new email address.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the email update operation.</returns>
        [HttpPut("change-email")]
        public async Task<IActionResult> Put([FromBody] QueryAppUserEmail reqUserEmail)
        {
            try
            {
                await _appUserService.PutAppUserEmailAsync(reqUserEmail);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Invalid operation");
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
