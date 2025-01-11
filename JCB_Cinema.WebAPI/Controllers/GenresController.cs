using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing and retrieving movie genres.
    /// </summary>
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="genreService">Service to handle operations related to genres.</param>
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Retrieves the list of genres.
        /// </summary>
        /// <returns>
        ///   * Status200OK (with data): If genres are successfully retrieved, the method returns a 200 OK response with the genre data.
        ///   * Status404NotFound (no data): If no genres are found.
        ///   * Status400BadRequest (no data): If there is an error while retrieving the genres.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var request = await _genreService.Get();
                return request == null ? NotFound() : Ok(request);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
