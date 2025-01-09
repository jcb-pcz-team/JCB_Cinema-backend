namespace JCB_Cinema.Application.DTOs
{
    public class BookingTicketDTO
    {
        public int BookingId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string? ScreenType { get; set; }
        public DateTime ScreenignTime { get; set; }
        public string CienemaHall { get; set; } = null!;
        public int SeatNumber { get; set; }
        public int MovieProjectionId { get; set; }
    }
}
