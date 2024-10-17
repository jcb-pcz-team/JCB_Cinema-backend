using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Domain.Entities
{
    public class MovieProjection : EntityBase
    {
        public int MovieProjectionId { get; set; }  // Klucz główny
        public Movie Movie { get; set; } = null!;
        public int MovieId { get; set; }  // Klucz obcy - film
        public Photo Photo { get; set; } = null!;
        public int PhotoId { get; set; }
        public DateTime ScreeningTime { get; set; }  // Czas projekcji
        public ScreenType ScreenType { get; set; } // Typ ekranu

        // Klucz obcy do sali kinowej
        public int HallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;  // Nawigacja do sali kinowej

        // Automatycznie zliczana liczba zajętych miejsc
        public int OccupiedSeats
        {
            get
            {
                return CinemaHall.Seats.Count(s => s.BookingTickets.Any(bt => bt.MovieProjectionId == MovieProjectionId));
            }
        }

        // Liczba wolnych miejsc
        public int AvailableSeats
        {
            get
            {
                return CinemaHall.TotalSeats.HasValue ? CinemaHall.TotalSeats.Value - OccupiedSeats : 0;
            }
        }
        public override int Key => MovieProjectionId;
    }
}
