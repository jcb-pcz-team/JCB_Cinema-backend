using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
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
        public async Task<IActionResult> Get([FromQuery] QueryMovieProjections request)
        {
            try
            {
                var result = await _movieProjectionService.Get(request);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Error occured");
            }
        }

        [HttpGet("{projectionId}")]
        public async Task<IActionResult> GetDetails(int projectionId)
        {
            try
            {
                var req = await _movieProjectionService.GetDetails(projectionId);
                return req == null ? NotFound() : Ok(req);
            }
            catch
            {
                return BadRequest("Error occured");
            }
        }
    }
}
