using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
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
        public async Task<IActionResult> Get([FromQuery] RequestAppUser request)
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
        public async Task<IActionResult> Put([FromQuery] PutAppUserDetails reqUser)
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
    }
}
