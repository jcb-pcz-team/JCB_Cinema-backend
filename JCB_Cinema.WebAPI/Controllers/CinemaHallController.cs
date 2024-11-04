using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/cinemahalls/{cinemaHallId}/movieprojections")]
    [ApiController]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallService _cinemaHallService;

        public CinemaHallController(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int cinemaHallId)
        {
            try
            {
                var projections = await _cinemaHallService.Get(cinemaHallId);
                return projections == null ? NotFound() : Ok(projections);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
