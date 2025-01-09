using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetFilteredSchedulesAsync([FromQuery] QuerySchedule request)
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

        [Authorize]
        [HttpGet("detailed")]
        public async Task<IActionResult> GetSchedulesDetails([FromQuery] QuerySchedule query)
        {
            try
            {
                return Ok(await _scheduleService.GetDetailedSchedules(query));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
