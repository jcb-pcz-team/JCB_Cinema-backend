namespace JCB_Cinema.Domain.Entities
{
    public class Seat : EntityBase
    {
        public int SeatId { get; set; }
        public int Number { get; set; }
        public int HallId { get; set; }
        public CinemaHall Hall { get; set; } = null!;
        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>(); public override int Key => SeatId;
    }
}
