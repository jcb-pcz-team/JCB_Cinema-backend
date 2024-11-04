using JCB_Cinema.Application.Interfaces;
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
        public async Task<IActionResult> GetFilteredSchedulesAsync([FromQuery] string date)
        {
            try
            {
                var request = await _scheduleService.GetFilteredSchedulesAsync(date);
                return request == null ? NotFound() : Ok(request);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
