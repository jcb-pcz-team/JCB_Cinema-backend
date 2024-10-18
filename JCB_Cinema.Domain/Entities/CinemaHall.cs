using System.ComponentModel.DataAnnotations.Schema;

namespace JCB_Cinema.Domain.Entities
{
    public class CinemaHall : EntityBase
    {
        public int CinemaHallId { get; set; }
        public string Name { get; set; } = null!;
        public List<Seat> Seats { get; set; } = new List<Seat>();

        [NotMapped]
        public int? TotalSeats => Seats.Count;
        public override int Key => CinemaHallId;
    }
}
