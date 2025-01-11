namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a booking ticket in the cinema system.
    /// </summary>
    public class BookingTicket : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking ticket.
        /// </summary>
        public int BookingTicketId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who made the booking.
        /// This is a foreign key to the <see cref="AppUser"/> entity.
        /// </summary>
        public string AppUserId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user who made the booking.
        /// </summary>
        public AppUser AppUser { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the associated movie projection.
        /// This is a foreign key to the <see cref="MovieProjection"/> entity.
        /// </summary>
        public int MovieProjectionId { get; set; }

        /// <summary>
        /// Gets or sets the movie projection associated with the booking.
        /// </summary>
        public MovieProjection MovieProjection { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the seat reserved for the booking.
        /// This is a foreign key to the <see cref="Seat"/> entity.
        /// </summary>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the seat reserved for the booking.
        /// </summary>
        public Seat Seat { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date and time when the booking was made.
        /// </summary>
        public DateTime ReservationTime { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the booking.
        /// If the booking is not confirmed by this time, it may be canceled.
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the booking is confirmed.
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the price of the booking ticket.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets the primary key of the booking ticket.
        /// This overrides the <see cref="EntityBase.Key"/> property.
        /// </summary>
        public override object Key => BookingTicketId;
    }
}
