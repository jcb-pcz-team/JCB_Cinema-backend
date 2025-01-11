namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a seat within a cinema hall.
    /// </summary>
    public class Seat : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the seat.
        /// </summary>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the number of the seat within the cinema hall.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the cinema hall to which the seat belongs.
        /// </summary>
        public int CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the cinema hall that contains this seat.
        /// </summary>
        public CinemaHall CinemaHall { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of booking tickets associated with this seat.
        /// </summary>
        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();

        /// <summary>
        /// Gets the unique key for the seat entity.
        /// </summary>
        public override object Key => SeatId;
    }
}
