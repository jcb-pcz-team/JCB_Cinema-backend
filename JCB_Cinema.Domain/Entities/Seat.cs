namespace JCB_Cinema.Domain.Entities
{
    public class Seat : EntityBase
    {
        public int SeatId { get; set; }  // Klucz główny dla miejsca
        public int Number { get; set; }  // Numer miejsca
        public int HallId { get; set; }  // Klucz obcy do sali kinowej
        public CinemaHall Hall { get; set; } = null!; // Nawigacja do sali kinowej

        // Lista przypisanych rezerwacji (odniesienia do biletów/rezerwacji)
        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>(); public override int Key => SeatId;
    }

    /*    //koncepcja sp czy jest wolne miejsce
        public bool IsSeatAvailable(int seatId, int movieProjectionId)
        {
            // Pobieramy miejsce z bazy danych
            Seat seat = _context.Seats
                .Include(s => s.BookingTickets)  // Dołączamy powiązane rezerwacje
                .FirstOrDefault(s => s.SeatId == seatId);

            if (seat == null)
            {
                throw new Exception("Seat not found.");
            }

            // Sprawdzamy, czy to miejsce jest już zarezerwowane dla danego seansu
            bool isOccupied = seat.BookingTickets
                .Any(bt => bt.MovieProjectionId == movieProjectionId && bt.IsConfirmed);

            // Zwracamy negację, bo jeśli jest zajęte, to nie jest dostępne
            return !isOccupied;
        }
    */
}
