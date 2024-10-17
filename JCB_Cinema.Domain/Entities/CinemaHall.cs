namespace JCB_Cinema.Domain.Entities
{
    public class CinemaHall : EntityBase
    {
        public int CinemaHallId { get; set; }
        public string Name { get; set; } = null!;
        public int? TotalSeats { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();

        public override int Key => CinemaHallId;
    }
}
