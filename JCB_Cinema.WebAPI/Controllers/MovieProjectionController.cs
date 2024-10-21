using JCB_Cinema.Application.Interfaces.Servicies;
using JCB_Cinema.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/moviesprojection")]
    [ApiController]
    public class MovieProjectionController : ControllerBase
    {
        private readonly IMovieProjectionService _movieProjectionService;

        public MovieProjectionController(IMovieProjectionService movieProjectionService)
        {
            _movieProjectionService = movieProjectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string screenType)
        {
            try
            {
                var ret = await _movieProjectionService.Get(screenType);
                return Ok(ret);
            }
            catch
            {
                return BadRequest("Error occured");
            }
        }
    }
}
