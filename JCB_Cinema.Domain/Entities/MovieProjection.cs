namespace JCB_Cinema.Domain.Entities
{
    public class MovieProjection : EntityBase
    {
        public int MovieProjectionId { get; set; }  // Klucz główny
        public int MovieId { get; set; }  // Klucz obcy - film
        public DateTime ScreeningTime { get; set; }  // Czas projekcji

        // Klucz obcy do sali kinowej
        public int HallId { get; set; }
        public CinemaHall Hall { get; set; } = null!;  // Nawigacja do sali kinowej

        // Automatycznie zliczana liczba zajętych miejsc
        public int OccupiedSeats
        {
            get
            {
                return Hall.Seats.Count(s => s.BookingTickets.Any(bt => bt.MovieProjectionId == MovieProjectionId));
            }
        }

        // Liczba wolnych miejsc
        public int AvailableSeats
        {
            get
            {
                return Hall.TotalSeats.HasValue ? Hall.TotalSeats.Value - OccupiedSeats : 0;
            }
        }
        public override int Key => MovieProjectionId;
    }
}
