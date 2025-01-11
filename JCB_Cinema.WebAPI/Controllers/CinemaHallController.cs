using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing cinema halls and their associated movie projections.
    /// </summary>
    [Route("api/cinemahalls")]
    [ApiController]
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IMovieProjectionService _movieProjectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CinemaHallController"/> class.
        /// </summary>
        /// <param name="cinemaHallService">Service to handle cinema hall operations such as retrieval and validation.</param>
        /// <param name="movieProjectionService">Service to handle movie projection operations associated with cinema halls.</param>
        public CinemaHallController(ICinemaHallService cinemaHallService, IMovieProjectionService movieProjectionService)
        {
            _cinemaHallService = cinemaHallService;
            _movieProjectionService = movieProjectionService;
        }

        /// <summary>
        /// Gets the movie projections for a specific cinema hall.
        /// </summary>
        /// <param name="id">The unique identifier of the cinema hall.</param>
        /// <returns>
        ///   * Status200OK (with data): If movie projections for the cinema hall are found, the method returns a 200 OK response with the projections data.
        ///   * Status404NotFound (no data): If the cinema hall with the specified ID does not exist.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the movie projections.
        /// </returns>
        [HttpGet("{id}/movieprojections")]
        public async Task<IActionResult> GetMovieProjections(int id)
        {
            try
            {
                // Check if the cinema hall exists
                if (!await _cinemaHallService.IsAny(a => a.CinemaHallId == id))
                {
                    return NotFound("No Cinema Hall Found");
                }
                // Retrieve the movie projections for the cinema hall
                return Ok(await _movieProjectionService.Get(new QueryMovieProjections { CinemaHallId = id }));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Gets a list of cinema halls based on the provided query parameters.
        /// </summary>
        /// <param name="request">A <see cref="QueryCinemaHall"/> object containing query parameters for retrieving cinema halls.</param>
        /// <returns>
        ///   * Status200OK (with data): If cinema halls are successfully retrieved, the method returns a 200 OK response with the list of cinema halls.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the cinema halls.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryCinemaHall request)
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
