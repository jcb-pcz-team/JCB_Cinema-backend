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

        [HttpGet("api/users/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var req = await _appUserService.GetAppUserAsync(userId);
                return req == null ? NotFound() : Ok(req);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("api/users/{userId}")]
        public async Task<IActionResult> Put(RequestAppUser reqUser)
        {
            try
            {
                await _appUserService.PutAppUserAsync(reqUser);
                return Created();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
