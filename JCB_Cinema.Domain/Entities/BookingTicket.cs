namespace JCB_Cinema.Domain.Entities
{
    public class BookingTicket : EntityBase
    {
        public int BookingTicketId { get; set; }

        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int MovieProjectionId { get; set; }
        public MovieProjection MovieProjection { get; set; } = null!;


        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;

        public DateTime ReservationTime { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsConfirmed { get; set; }
        public int Price { get; set; }

        public override object Key => BookingTicketId;
    }
}
