using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryMovies request)
        {
            try
            {
                return Ok(await _movieService.Get(request));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDetails(string title)
        {
            try
            {
                if (!await _movieService.IsAny(m => m.Title == title))
                {
                    return NotFound("No Movie Found");
                }
                return Ok(await _movieService.GetDetails(title));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            try
            {
                var result = await _movieService.GetUpcoming();
                if (result == null || result.Count == 0)
                    return NotFound("No upcoming premieres");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
