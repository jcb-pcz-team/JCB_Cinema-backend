using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovieProjection([FromQuery] AddMovieProjectionRequest request)
        {
            try
            {
                await _movieProjectionService.AddMovieProjection(request);
                return Created();
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{projectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovieProjection(int projectionId, [FromQuery] UpdateMovieProjectionRequest request)
        {
            try
            {
                await _movieProjectionService.UpdateMovieProjection(projectionId, request);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovieProjection(int id)
        {
            try
            {
                await _movieProjectionService.DeleteMovieProjection(id);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You a're unauthorized");
            }
            catch (NullReferenceException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
