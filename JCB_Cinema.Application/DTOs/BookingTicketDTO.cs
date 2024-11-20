using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.DTOs
{
    public class BookingTicketDTO
    {
        public string MovieTitle { get; set; } = null!;
        public ScreenType ScreenType { get; set; }
        public DateTime ScreenignTime { get; set; }
        public CinemaHall CienemaHall { get; set; } = null!;
        public int SeatNumber { get; set; }
        public string? UserName { get; set; }
        public string? BookingURL { get; set; }
    }
}
