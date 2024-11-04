using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        
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
