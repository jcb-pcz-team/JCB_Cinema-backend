using JCB_Cinema.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a movie projection, detailing the movie, its screening time, location, and ticket pricing.
    /// </summary>
    public class MovieProjection : EntityBase
    {
        private readonly int _basePrice = 2250; // Base ticket price in cents (PLN).

        /// <summary>
        /// Gets or sets the unique identifier for the movie projection.
        /// </summary>
        public int MovieProjectionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the associated movie.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the movie associated with the projection.
        /// </summary>
        public Movie? Movie { get; set; }

        /// <summary>
        /// Gets or sets the screening time for the movie projection.
        /// </summary>
        public DateTime ScreeningTime { get; set; }

        private ScreenType _screenType;

        /// <summary>
        /// Gets or sets the screen type for the movie projection (e.g., 2D, 3D, IMAX).
        /// Updates the ticket price based on the screen type.
        /// </summary>
        public ScreenType ScreenType
        {
            get => _screenType;
            set
            {
                _screenType = value;
                UpdatePrice(); // Update ticket price when the screen type changes.
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the cinema hall where the movie is being shown.
        /// </summary>
        public int CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the cinema hall associated with the movie projection.
        /// </summary>
        public CinemaHall? CinemaHall { get; set; }

        /// <summary>
        /// Gets the normalized title of the associated movie.
        /// Returns an empty string if no movie is associated.
        /// </summary>
        [NotMapped]
        public string MovieNormalizedTitle => Movie == null ? "" : Movie.NormalizedTitle;

        /// <summary>
        /// Gets the ticket price for the movie projection. Price is stored in cents.
        /// </summary>
        public Price Price { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieProjection"/> class.
        /// Sets default screen type to 2D and calculates the initial ticket price.
        /// </summary>
        public MovieProjection()
        {
            _screenType = ScreenType.TwoD; // Default screen type.
            Price = new Price(_basePrice + CalculateSubchargeForTicketPrice(_screenType), "pln");
        }

        /// <summary>
        /// Gets the count of occupied seats for the movie projection.
        /// Returns 0 if the cinema hall is null.
        /// </summary>
        [NotMapped]
        public int OccupiedSeats => CinemaHall == null ? 0 : CinemaHall.Seats.Count(s => s.BookingTickets.Any(bt => bt.MovieProjectionId == MovieProjectionId));

        /// <summary>
        /// Gets the count of available seats for the movie projection.
        /// Returns 0 if the cinema hall or total seats are null.
        /// </summary>
        [NotMapped]
        public int AvailableSeats => CinemaHall == null ? 0 : CinemaHall.TotalSeats.HasValue ? CinemaHall.TotalSeats.Value - OccupiedSeats : 0;

        /// <summary>
        /// Gets the unique key for the movie projection entity.
        /// </summary>
        public override object Key => MovieProjectionId;

        /// <summary>
        /// Updates the ticket price based on the screen type.
        /// </summary>
        private void UpdatePrice()
        {
            Price = new Price(_basePrice + CalculateSubchargeForTicketPrice(_screenType), "pln");
        }

        /// <summary>
        /// Calculates the surcharge for a ticket based on the screen type.
        /// </summary>
        /// <param name="screenType">The screen type for which the surcharge is to be calculated.</param>
        /// <returns>The calculated surcharge in cents.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the screen type is invalid.</exception>
        public static int CalculateSubchargeForTicketPrice(ScreenType screenType = ScreenType.TwoD)
        {
            int surcharge = 0;

            switch (screenType)
            {
                case ScreenType.TwoD:
                    surcharge = 0;
                    break;
                case ScreenType.ThreeD:
                    surcharge = 690;
                    break;
                case ScreenType.IMAX:
                    surcharge = 1230;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null);
            }

            return surcharge; // Returns the calculated surcharge for the ticket.
        }
    }
}
