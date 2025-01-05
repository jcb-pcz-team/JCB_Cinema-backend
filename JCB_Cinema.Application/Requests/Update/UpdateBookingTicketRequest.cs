using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.Requests.Update
{
    public class UpdateBookingTicketRequest
    {
        [Required]
        public int BookingTicketId { get; set; }
        public int MovieProjectionId { get; set; }
        public int SeatId { get; set; }
    }
}
