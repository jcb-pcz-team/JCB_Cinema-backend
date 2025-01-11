using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing screen types in the cinema system.
    /// </summary>
    [Route("api/movies/types")]
    [ApiController]
    public class ScreenTypeController : ControllerBase
    {
        private readonly IScreenTypeService _ScreenTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenTypeController"/> class.
        /// </summary>
        /// <param name="ScreenTypeervice">Service for handling screen type operations.</param>
        public ScreenTypeController(IScreenTypeService ScreenTypeervice)
        {
            _ScreenTypeService = ScreenTypeervice;
        }

        /// <summary>
        /// Retrieves a list of available screen types.
        /// </summary>
        /// <returns>
        ///   * Status200OK (with data): If the list of screen types is found, returns a 200 OK response with the list of screen types.
        ///   * Status404NotFound (no data): If no screen types are found.
        ///   * Status400BadRequest (no data): If there is an error while retrieving screen types.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var request = await _ScreenTypeService.Get();
                return request == null ? NotFound() : Ok(request);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
