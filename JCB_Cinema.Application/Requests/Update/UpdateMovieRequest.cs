namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to update the details of a movie.
    /// </summary>
    public class UpdateMovieRequest
    {
        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        public DateOnly ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie (e.g., Drama, Comedy, etc.).
        /// </summary>
        public string Genre { get; set; } = null!;

        /// <summary>
        /// Gets or sets a flag indicating whether to set the previous poster for the movie. 
        /// If true, the previous poster will be used. Defaults to true.
        /// </summary>
        public bool? SetPreviousPoster { get; set; } = true;
    }
}
