using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

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
