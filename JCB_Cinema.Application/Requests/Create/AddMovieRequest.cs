namespace JCB_Cinema.Application.Requests.Create
{
    /// <summary>
    /// Represents a request to add a new movie.
    /// </summary>
    public class AddMovieRequest
    {
        /// <summary>
        /// Gets or sets the title of the movie.
        /// This field is required.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the movie.
        /// This field is required.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// This field is required.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// This field is required.
        /// </summary>
        public DateOnly ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie (e.g., Action, Drama).
        /// This field is required.
        /// </summary>
        public string Genre { get; set; } = null!;
    }
}
