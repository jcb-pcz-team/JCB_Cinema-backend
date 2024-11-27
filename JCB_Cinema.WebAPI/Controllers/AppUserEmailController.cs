using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [ApiController]
    [Route("api/users/change-email")]
    [Authorize]

    public class AppUserEmailController : ControllerBase
    {
        private readonly IAppUserEmailService _appUserEmailService;

        public AppUserEmailController(IAppUserEmailService appUserEmailService)
        {
            _appUserEmailService = appUserEmailService;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] RequestAppUserEmail reqUserEmail)
        {
            try
            {
                await _appUserEmailService.PutAppUserEmailAsync(reqUserEmail);
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
