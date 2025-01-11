using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing schedules in the cinema system.
    /// </summary>
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleController"/> class.
        /// </summary>
        /// <param name="scheduleService">Service for handling schedule operations.</param>
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Retrieves a filtered list of schedules.
        /// </summary>
        /// <param name="request">The query parameters for filtering schedules.</param>
        /// <returns>
        ///   * Status200OK (with data): If the filtered schedules are found, returns a 200 OK response with the list of schedules.
        ///   * Status400BadRequest (no data): If there is an error while retrieving schedules.
        /// </returns>
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

        /// <summary>
        /// Retrieves detailed schedules (requires user authentication).
        /// </summary>
        /// <param name="query">The query parameters for retrieving detailed schedules.</param>
        /// <returns>
        ///   * Status200OK (with data): If the detailed schedules are found, returns a 200 OK response with the schedule details.
        ///   * Status401Unauthorized (no data): If the user is not authorized to access the detailed schedules.
        ///   * Status404NotFound (no data): If no detailed schedules are found for the specified query.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the schedules.
        /// </returns>
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
