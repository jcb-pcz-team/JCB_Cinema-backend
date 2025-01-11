using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing movies in the cinema.
    /// </summary>
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="movieService">Service to handle operations related to movies.</param>
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves a list of movies based on specified query parameters.
        /// </summary>
        /// <param name="request">The query parameters for fetching movies.</param>
        /// <returns>
        ///   * Status200OK (with data): If the request is successful, the method returns a 200 OK response with movie data.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the movies.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryMovies request)
        {
            try
            {
                return Ok(await _movieService.Get(request));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Retrieves a list of movie titles.
        /// </summary>
        /// <returns>
        ///   * Status200OK (with data): If the request is successful, the method returns a 200 OK response with movie titles.
        ///   * Status400BadRequest (no data): If there is an error while retrieving movie titles.
        /// </returns>
        [HttpGet("titles")]
        public async Task<IActionResult> GetMoviesTittles()
        {
            try
            {
                return Ok(await _movieService.GetTitles());
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Retrieves detailed information about a specific movie based on its title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <returns>
        ///   * Status200OK (with data): If the movie is found, the method returns a 200 OK response with movie details.
        ///   * Status404NotFound (no data): If no movie is found with the provided title.
        /// </returns>
        [HttpGet("{title}")]
        public async Task<IActionResult> GetDetails(string title)
        {
            try
            {
                if (!await _movieService.IsAny(m => m.NormalizedTitle == title))
                {
                    return NotFound("No Movie Found");
                }
                return Ok(await _movieService.GetDetails(title));
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Retrieves a list of upcoming movie premieres.
        /// </summary>
        /// <returns>
        ///   * Status200OK (with data): If the request is successful, the method returns a 200 OK response with a list of upcoming premieres.
        ///   * Status404NotFound (no data): If no upcoming movie premieres are found.
        /// </returns>
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming()
        {
            try
            {
                var result = await _movieService.GetUpcoming();
                if (result == null || result.Count == 0)
                    return NotFound("No upcoming premieres");
                return Ok(result);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Adds a new movie to the database.
        /// </summary>
        /// <param name="movie">The details of the movie to be added.</param>
        /// <returns>
        ///   * Status201Created (with data): If the movie is successfully added, the method returns a 201 Created response with the movie's title.
        ///   * Status401Unauthorized (no data): If the user is not authorized to add a movie.
        ///   * Status400BadRequest (no data): If there is an error while adding the movie.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest movie)
        {
            try
            {
                var title = await _movieService.AddMovie(movie);
                return CreatedAtAction(nameof(GetDetails), new { title = title }, title);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Updates an existing movie in the database.
        /// </summary>
        /// <param name="title">The title of the movie to be updated.</param>
        /// <param name="movie">The updated details of the movie.</param>
        /// <returns>
        ///   * Status200OK (no data): If the movie is successfully updated, the method returns a 200 OK response.
        ///   * Status401Unauthorized (no data): If the user is not authorized to update the movie.
        ///   * Status400BadRequest (no data): If there is an error while updating the movie.
        /// </returns>
        [HttpPut("update/{title}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(string title, [FromBody] UpdateMovieRequest movie)
        {
            try
            {
                await _movieService.UpdateMovie(title, movie);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }

        /// <summary>
        /// Deletes an existing movie from the database.
        /// </summary>
        /// <param name="title">The title of the movie to be deleted.</param>
        /// <returns>
        ///   * Status204NoContent (no data): If the movie is successfully deleted.
        ///   * Status401Unauthorized (no data): If the user is not authorized to delete the movie.
        ///   * Status404NotFound (no data): If no movie with the specified title is found.
        ///   * Status400BadRequest (no data): If there is an error while deleting the movie.
        /// </returns>
        [HttpDelete("delete/{title}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(string title)
        {
            try
            {
                await _movieService.DeleteMovie(title);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
