using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize]
    public class BookingTicketController : ControllerBase
    {
        private readonly IBookingTicketService _bookingTicketService;

        public BookingTicketController(IBookingTicketService bookingTicketService)
        {
            _bookingTicketService = bookingTicketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserReservationHistory([FromQuery] QueryAppUser reqAppUser)
        {
            try
            {
                // if null then display empty page
                return Ok(await _bookingTicketService.GetUserBookingHistoryAsync(reqAppUser));
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
    }
}