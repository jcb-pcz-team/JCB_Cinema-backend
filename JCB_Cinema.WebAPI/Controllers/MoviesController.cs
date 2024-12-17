using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{title}")]
        public async Task<IActionResult> GetDetails(string title)
        {
            try
            {
                if (!await _movieService.IsAny(m => m.NormalizedTitle == title))
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromQuery] AddMovieRequest movie)
        {
            try
            {
                var title = await _movieService.AddMovie(movie);

                return CreatedAtAction(nameof(GetDetails), new { title = title }, title);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpPut("update/{title}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(string title, [FromQuery] UpdateMovieDTO movie)
        {
            try
            {
                await _movieService.UpdateMovie(title, movie);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpDelete("delete/{title}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(string title)
        {
            try
            {
                await _movieService.DeleteMovie(title);
                return NoContent();
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
