namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a booking ticket.
    /// </summary>
    public class BookingTicketDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the booking ID.
        /// </value>
        public int BookingId { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie for the booking.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the movie title.
        /// </value>
        public string MovieTitle { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type of the screen where the movie is being shown.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the screen type (e.g., IMAX, standard).
        /// </value>
        public string? ScreenType { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the movie screening.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> representing the screening time.
        /// </value>
        public DateTime ScreenignTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the cinema hall where the movie is being shown.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the cinema hall name.
        /// </value>
        public string CienemaHall { get; set; } = null!;

        /// <summary>
        /// Gets or sets the seat number for the booking.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the seat number in the cinema hall.
        /// </value>
        public int SeatNumber { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the movie projection.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the movie projection ID.
        /// </value>
        public int MovieProjectionId { get; set; }
    }
}
