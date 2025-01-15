using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing movie projections in the cinema.
    /// </summary>
    [Route("api/moviesprojection")]
    [ApiController]
    public class MovieProjectionController : ControllerBase
    {
        private readonly IMovieProjectionService _movieProjectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieProjectionController"/> class.
        /// </summary>
        /// <param name="movieProjectionService">Service to handle operations related to movie projections.</param>
        public MovieProjectionController(IMovieProjectionService movieProjectionService)
        {
            _movieProjectionService = movieProjectionService;
        }

        /// <summary>
        /// Retrieves a list of movie projections based on specified query parameters.
        /// </summary>
        /// <param name="request">The query parameters for fetching movie projections.</param>
        /// <returns>
        ///   * Status200OK (with data): If the request is successful, the method returns a 200 OK response with movie projections.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the projections.
        /// </returns>
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
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Retrieves detailed information about a specific movie projection.
        /// </summary>
        /// <param name="projectionId">The ID of the movie projection.</param>
        /// <returns>
        ///   * Status200OK (with data): If the movie projection is found, the method returns a 200 OK response with projection details.
        ///   * Status404NotFound (no data): If the movie projection is not found.
        /// </returns>
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
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Adds a new movie projection.
        /// </summary>
        /// <param name="request">The details of the movie projection to be added.</param>
        /// <returns>
        ///   * Status201Created (no data): If the movie projection is successfully added, the method returns a 201 Created response.
        ///   * Status401Unauthorized (no data): If the user is not authorized to add a movie projection.
        ///   * Status400BadRequest (no data): If there is an error while adding the movie projection.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovieProjection([FromBody] AddMovieProjectionRequest request)
        {
            try
            {
                await _movieProjectionService.AddMovieProjection(request);
                return Created();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Updates an existing movie projection.
        /// </summary>
        /// <param name="projectionId">The ID of the movie projection to be updated.</param>
        /// <param name="request">The updated details of the movie projection.</param>
        /// <returns>
        ///   * Status204NoContent (no data): If the movie projection is successfully updated, the method returns a 204 No Content response.
        ///   * Status401Unauthorized (no data): If the user is not authorized to update the movie projection.
        ///   * Status400BadRequest (no data): If there is an error while updating the movie projection.
        /// </returns>
        [HttpPut("{projectionId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovieProjection(int projectionId, [FromBody] UpdateMovieProjectionRequest request)
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

        /// <summary>
        /// Deletes an existing movie projection.
        /// </summary>
        /// <param name="id">The ID of the movie projection to be deleted.</param>
        /// <returns>
        ///   * Status204NoContent (no data): If the movie projection is successfully deleted.
        ///   * Status401Unauthorized (no data): If the user is not authorized to delete the movie projection.
        ///   * Status400BadRequest (no data): If there is an error while deleting the movie projection.
        /// </returns>
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
                return Unauthorized("You're unauthorized");
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

        [HttpGet("{movieProjectionId}/GetSeatsStatus")]
        [Authorize]
        public async Task<IActionResult> GetSeatsStatus(int movieProjectionId)
        {
            try
            {
                return Ok(await _movieProjectionService.SeatsStatus(movieProjectionId));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You're unauthorized");
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
