using JCB_Cinema.Application.Interfaces;
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
        public async Task<IActionResult> Get([FromQuery] int? genreId, [FromQuery] string? genreName)
        {
            try
            {
                if (genreId.HasValue)
                {
                    var request = await _movieService.Get(genreId.Value);
                    return request == null ? NotFound() : Ok(request);
                }
                else if (!string.IsNullOrWhiteSpace(genreName))
                {
                    var request = await _movieService.Get(genreName);
                    return request == null ? NotFound() : Ok(request);
                }
                else
                {
                    return BadRequest("Either genreId or genreName must be provided.");
                }
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
