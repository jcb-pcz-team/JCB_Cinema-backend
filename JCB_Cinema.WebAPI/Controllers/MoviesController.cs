using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces.Servicies;
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
        public async Task<IActionResult> Get([FromQuery] RequestMovies query)
        {
            try
            {
                return Ok(await _movieService.Get(query));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
