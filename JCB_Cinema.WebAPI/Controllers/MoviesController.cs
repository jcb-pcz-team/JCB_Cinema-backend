using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
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

        [HttpGet("api/movies")]
        public async Task<IActionResult> Get([FromQuery] RequestMovies request)
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

        [HttpGet("api/movies/{Id}")]
        public async Task<IActionResult> GetDetails(int Id)
        {
            try
            {
                if (!await _movieService.IsAny(m => m.MovieId == Id))
                {
                    return NotFound("No Movie Found");
                }
                return Ok(await _movieService.GetDetails(Id));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpGet("api/movies/upcoming")]
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
