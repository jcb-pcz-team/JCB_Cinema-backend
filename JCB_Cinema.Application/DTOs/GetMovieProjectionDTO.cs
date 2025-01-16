namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a movie projection.
    /// </summary>
    public class GetMovieProjectionDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the movie projection.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the unique ID for the movie projection.
        /// </value>
        public int MovieProjectionId { get; set; }

        /// <summary>
        /// Gets or sets the movie associated with the projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="GetMovieDTO"/> representing the movie for this projection.
        /// This property is initialized as read-only.
        /// </value>
        public GetMovieDTO? Movie { get; init; }

        /// <summary>
        /// Gets or sets the screening time of the movie projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="DateTime"/> representing the date and time of the movie screening.
        /// This property is initialized as read-only.
        /// </value>
        public DateTime? ScreeningTime { get; init; }

        /// <summary>
        /// Gets or sets the type of screen where the movie is projected (e.g., IMAX, Standard).
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the screen type.
        /// This property is initialized as read-only.
        /// </value>
        public string? ScreenType { get; init; }

        /// <summary>
        /// Gets or sets the cinema hall where the movie projection occurs.
        /// </summary>
        /// <value>
        /// A nullable <see cref="GetCinemaHallDTO"/> representing the cinema hall for this projection.
        /// </value>
        public GetCinemaHallDTO? CinemaHall { get; set; }

        /// <summary>
        /// Gets or sets the normalized title of the movie for this projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the normalized movie title, which may be used for search or comparison.
        /// </value>
        public string? NormalizedMovieTitle { get; set; }

        /// <summary>
        /// Gets or sets the pricing information for the movie projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="GetPriceDTO"/> representing the price details for the projection.
        /// This property is initialized as read-only.
        /// </value>
        public GetPriceDTO? Price { get; init; }

        /// <summary>
        /// Gets or sets the number of occupied seats for the movie projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="int"/> representing the count of occupied seats.
        /// </value>
        public int? OccupiedSeats { get; set; }

        /// <summary>
        /// Gets or sets the number of available seats for the movie projection.
        /// </summary>
        /// <value>
        /// A nullable <see cref="int"/> representing the count of available seats for the projection.
        /// </value>
        public int? AvailableSeats { get; set; }
    }
}
