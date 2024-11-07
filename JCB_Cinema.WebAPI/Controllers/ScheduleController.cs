using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredSchedulesAsync([FromQuery] RequestSchedule request)
        {
            try
            {
                return Ok(await _scheduleService.Get(request));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
