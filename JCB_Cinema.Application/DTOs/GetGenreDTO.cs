namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a genre in the cinema application.
    /// </summary>
    public class GetGenreDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the genre's unique ID.
        /// </value>
        public int GenreId { get; set; }

        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the name of the genre.
        /// This field is initialized to an empty string.
        /// </value>
        public string GenreName { get; set; } = string.Empty;
    }
}
