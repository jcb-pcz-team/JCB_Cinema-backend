using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to update the details of a booking ticket.
    /// </summary>
    public class UpdateBookingTicketRequest
    {
        /// <summary>
        /// Gets or sets the identifier of the booking ticket to be updated.
        /// </summary>
        [Required]
        public int BookingTicketId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the movie projection associated with the booking.
        /// </summary>
        public int MovieProjectionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the seat assigned to the booking.
        /// </summary>
        public int SeatId { get; set; }
    }
}
