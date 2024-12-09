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

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromQuery] AddMovieDTO movie) 
        { 
            try
            {
                await _movieService.AddMovie(movie);
                return Created();
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

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie([FromQuery] UpdateMovieDTO movie)
        {
            try
            {
                await _movieService.UpdateMovie(movie);
                return Created();
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
    }
}
