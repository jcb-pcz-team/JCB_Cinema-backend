using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing booking tickets, including getting booking history, editing, and deleting tickets.
    /// </summary>
    [ApiController]
    [Route("api/bookings")]
    [Authorize]
    public class BookingTicketController : ControllerBase
    {
        private readonly IBookingTicketService _bookingTicketService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingTicketController"/> class.
        /// </summary>
        /// <param name="bookingTicketService">Service to handle booking ticket operations such as history retrieval, editing, and deletion.</param>
        public BookingTicketController(IBookingTicketService bookingTicketService)
        {
            _bookingTicketService = bookingTicketService;
        }

        /// <summary>
        /// Gets the booking history for the current user.
        /// </summary>
        /// <param name="reqAppUser">A <see cref="QueryAppUser"/> object containing the user's details.</param>
        /// <returns>
        ///   * Status200OK (with data): If the user's booking history is successfully retrieved, the method returns a 200 OK response with booking history data.
        ///   * Status401Unauthorized (no data): If the user is not authorized to access the booking history.
        ///   * Status400BadRequest (no data): If there is an error during the process of retrieving the booking history.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetUserReservationHistory([FromQuery] QueryAppUser reqAppUser)
        {
            try
            {
                // If null, return empty data
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

        /// <summary>
        /// Edits an existing booking ticket.
        /// </summary>
        /// <param name="request">A <see cref="UpdateBookingTicketRequest"/> object containing the updated details for the booking ticket.</param>
        /// <returns>
        ///   * Status204NoContent: If the booking ticket is successfully edited, the method returns a 204 No Content response.
        ///   * Status401Unauthorized (no data): If the user is not authorized to edit the booking ticket.
        ///   * Status404NotFound (no data): If the booking ticket to be edited does not exist.
        ///   * Status400BadRequest (no data): If there is an error during the process of editing the booking ticket.
        /// </returns>
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

        /// <summary>
        /// Deletes a booking ticket.
        /// </summary>
        /// <param name="id">The unique identifier of the booking ticket to delete.</param>
        /// <returns>
        ///   * Status204NoContent: If the booking ticket is successfully deleted, the method returns a 204 No Content response.
        ///   * Status401Unauthorized (no data): If the user is not authorized to delete the booking ticket.
        ///   * Status404NotFound (no data): If the booking ticket to delete does not exist.
        ///   * Status400BadRequest (no data): If there is an error during the deletion process.
        /// </returns>
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBookingTicket(string? userName, [FromBody] AddBookingTicketRequest request)
        {
            try
            {
                var id = await _bookingTicketService.AddBookingTicket(userName, request);
                return CreatedAtAction(nameof(GetBookingDetails), new { bookingId = id, userName = userName ?? "" }, id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (TimeoutException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bookingId}")]
        [Authorize]
        public async Task<IActionResult> GetBookingDetails(int bookingId, string? userName)
        {
            try
            {
                var result = await _bookingTicketService.GetBookingDetails(bookingId, userName);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (TimeoutException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm/{bookingId}")]
        [Authorize]
        public async Task<IActionResult> ConfirmBooking(int bookingId)
        {
            try
            {
                await _bookingTicketService.ConfirmBooking(bookingId);
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
            catch (TimeoutException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}