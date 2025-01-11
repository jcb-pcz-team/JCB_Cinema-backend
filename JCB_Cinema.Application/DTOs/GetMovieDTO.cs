namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a movie.
    /// </summary>
    public class GetMovieDTO
    {
        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the movie's title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing a brief description of the movie.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie in minutes.
        /// </summary>
        /// <value>
        /// A nullable <see cref="int"/> representing the movie's duration in minutes.
        /// </value>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        /// <value>
        /// A nullable <see cref="DateOnly"/> representing the movie's release date.
        /// </value>
        public DateOnly? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the genre of the movie.
        /// </summary>
        /// <value>
        /// A nullable <see cref="GetGenreDTO"/> representing the movie's genre.
        /// </value>
        public GetGenreDTO? Genre { get; set; }

        /// <summary>
        /// Gets or sets the normalized title of the movie.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing a normalized version of the movie's title (e.g., for searching).
        /// </value>
        public string? NormalizedTitle { get; set; }

        /// <summary>
        /// Gets or sets the release date, another date format for the movie release.
        /// </summary>
        /// <value>
        /// A nullable <see cref="DateOnly"/> representing an alternate release date for the movie.
        /// </value>
        public DateOnly? Release { get; set; }
    }
}
