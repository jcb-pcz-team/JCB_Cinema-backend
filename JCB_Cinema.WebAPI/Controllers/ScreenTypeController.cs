using JCB_Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [Route("api/movies/types")]
    [ApiController]
    public class ScreenTypeController : ControllerBase
    {
        private readonly IScreenTypeService _ScreenTypeService;
        public ScreenTypeController(IScreenTypeService ScreenTypeervice)
        {
            _ScreenTypeService = ScreenTypeervice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var request = await _ScreenTypeService.Get();
                return request == null ? NotFound() : Ok(request);
            }
            catch
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
