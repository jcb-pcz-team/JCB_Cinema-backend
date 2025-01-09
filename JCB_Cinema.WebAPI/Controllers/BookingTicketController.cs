using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
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

        [HttpPut]
        public async Task<IActionResult> EditBookingTicket([FromBody] UpdateBookingTicketRequest request)
        {
            try
            {
                await _bookingTicketService.EditBookingTicket(request);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingTicket(int id)
        {
            try
            {
                await _bookingTicketService.DeleteBookingTicket(id);
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