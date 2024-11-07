using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/cinemahalls")]
    [ApiController]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IMovieProjectionService _movieProjectionService;

        public CinemaHallController(ICinemaHallService cinemaHallService, IMovieProjectionService movieProjectionService)
        {
            _cinemaHallService = cinemaHallService;
            _movieProjectionService = movieProjectionService;
        }

        [HttpGet("{id}/movieprojections")]
        public async Task<IActionResult> GetMovieProjections(int id)
        {
            try
            {
                if (!await _cinemaHallService.IsAny(a => a.CinemaHallId == id))
                {
                    return NotFound("No Cinema Hall Found");
                }
                return Ok(await _movieProjectionService.Get(new RequestMovieProjection { CinemaHallId = id }));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestCinemaHall request)
        {
            try
            {
                return Ok(await _cinemaHallService.Get(request));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
