using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
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

        [HttpGet("api/moviesprojection")]
        public async Task<IActionResult> Get([FromQuery] RequestMovieProjection request)
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

        [HttpGet("api/moviesprojection/{projectionId}")]
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
