using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to update the details of a movie projection.
    /// </summary>
    public class UpdateMovieProjectionRequest
    {
        /// <summary>
        /// Gets or sets the normalized title of the movie associated with the projection.
        /// </summary>
        public string NormalizedTitle { get; set; } = null!;

        /// <summary>
        /// Gets or sets the screening time of the movie projection.
        /// </summary>
        public DateTime ScreeningTime { get; set; }

        /// <summary>
        /// Gets or sets the screen type for the movie projection (e.g., 2D, 3D).
        /// </summary>
        public ScreenType ScreenType { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the cinema hall where the movie will be projected.
        /// </summary>
        public int CinemaHallId { get; set; }
    }
}
