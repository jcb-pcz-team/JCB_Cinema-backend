namespace JCB_Cinema.Domain.Entities
{
    public class BookingTicket : EntityBase
    {
        public int BookingTicketId { get; set; }  // Klucz główny
        public int UserId { get; set; }  // Klucz obcy - użytkownik
        public User User { get; set; }  // Nawigacja do użytkownika
        public int MovieProjectionId { get; set; }  // Klucz obcy - MovieProjection
        public MovieProjection MovieProjection { get; set; } = null!;  // Nawigacja do projekcji filmu

        // Przypisane miejsce w sali kinowej
        public int SeatId { get; set; }  // Klucz obcy do miejsca
        public Seat Seat { get; set; } = null!;  // Nawigacja do miejsca

        public DateTime ReservationTime { get; set; }  // Czas rezerwacji
        public DateTime? ExpiresAt { get; set; }  // Data wygaśnięcia (dla rezerwacji)
        public bool IsConfirmed { get; set; }  // Czy rezerwacja została potwierdzona (czyli przekształcona w bilet)
        public int Price { get; set; }  // Cena biletu w groszach

        public override int Key => BookingTicketId;
    }
}
