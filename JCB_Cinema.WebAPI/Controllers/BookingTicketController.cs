using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing booking tickets, including getting booking history, adding, editing, deleting tickets, and confirming bookings.
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
        /// <param name="bookingTicketService">Service to handle booking ticket operations.</param>
        public BookingTicketController(IBookingTicketService bookingTicketService)
        {
            _bookingTicketService = bookingTicketService;
        }

        /// <summary>
        /// Gets the booking history for the current user.
        /// </summary>
        /// <param name="reqAppUser">The query object containing user details.</param>
        /// <returns>
        /// * 200 OK: Returns the booking history of the user.
        /// * 401 Unauthorized: If the user is not authorized to access the booking history.
        /// * 400 Bad Request: If an error occurs during the operation.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetUserReservationHistory([FromQuery] QueryAppUser reqAppUser)
        {
            try
            {
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
        /// <param name="request">The request object containing updated booking details.</param>
        /// <returns>
        /// * 204 No Content: If the booking ticket is successfully edited.
        /// * 401 Unauthorized: If the user is not authorized to edit the booking ticket.
        /// * 404 Not Found: If the booking ticket to edit does not exist.
        /// * 400 Bad Request: If an error occurs during the operation.
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
        /// Deletes a booking ticket by its ID.
        /// </summary>
        /// <param name="id">The ID of the booking ticket to delete.</param>
        /// <returns>
        /// * 204 No Content: If the booking ticket is successfully deleted.
        /// * 401 Unauthorized: If the user is not authorized to delete the booking ticket.
        /// * 404 Not Found: If the booking ticket to delete does not exist.
        /// * 400 Bad Request: If an error occurs during the operation.
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

        /// <summary>
        /// Adds a new booking ticket.
        /// </summary>
        /// <param name="userName">The name of the user adding the booking ticket.</param>
        /// <param name="request">The request object containing booking details.</param>
        /// <returns>
        /// * 201 Created: Returns the ID of the created booking ticket.
        /// * 401 Unauthorized: If the user is not authorized to add a booking ticket.
        /// * 404 Not Found: If any related entity (e.g., user or cinema hall) is not found.
        /// * 400 Bad Request: If an error occurs during the operation.
        /// </returns>
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

        /// <summary>
        /// Gets the details of a specific booking ticket.
        /// </summary>
        /// <param name="bookingId">The ID of the booking ticket.</param>
        /// <param name="userName">The name of the user requesting the booking details.</param>
        /// <returns>
        /// * 200 OK: Returns the booking details.
        /// * 401 Unauthorized: If the user is not authorized to view the booking details.
        /// * 404 Not Found: If the booking ticket does not exist.
        /// * 400 Bad Request: If an error occurs during the operation.
        /// </returns>
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

        /// <summary>
        /// Confirms a booking ticket by its ID.
        /// </summary>
        /// <param name="bookingId">The ID of the booking ticket to confirm.</param>
        /// <returns>
        /// * 204 No Content: If the booking ticket is successfully confirmed.
        /// * 401 Unauthorized: If the user is not authorized to confirm the booking ticket.
        /// * 404 Not Found: If the booking ticket does not exist.
        /// * 400 Bad Request: If an error occurs during the operation.
        /// </returns>
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