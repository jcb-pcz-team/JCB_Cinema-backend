using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredSchedulesAsync([FromQuery] string date)
        {
            try
            {
                return Ok(await _scheduleService.GetFilteredSchedulesAsync(date));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
