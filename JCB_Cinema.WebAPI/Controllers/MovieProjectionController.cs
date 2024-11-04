using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using Microsoft.AspNetCore.Authorization;
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
                var result = await _movieProjectionService.Get(screenType);
                return result == null ? NotFound() : Ok(result);
            }
            catch
            {
                return BadRequest("Error occured");
            }
        }
    }
}
