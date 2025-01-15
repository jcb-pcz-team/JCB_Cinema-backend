using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.Requests.Create
{
    /// <summary>
    /// Represents a request to add a new movie projection.
    /// </summary>
    public class AddMovieProjectionRequest
    {
        /// <summary>
        /// Gets or sets the screening time of the movie projection.
        /// </summary>
        [Required]
        public DateTime ScreeningTime { get; set; }

        /// <summary>
        /// Gets or sets the screen type for the movie projection (e.g., 2D, 3D, IMAX).
        /// </summary>
        [Required]
        public string ScreenType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the cinema hall where the movie will be projected.
        /// This field is required.
        /// </summary>
        [Required]
        public int CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the normalized title of the movie that will be projected.
        /// This field is required.
        /// </summary>
        [Required]
        public string MovieNormalizedTitle { get; set; } = string.Empty;
    }
}
