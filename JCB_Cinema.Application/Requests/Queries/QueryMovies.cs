namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for filtering movies based on various criteria such as genre and release date.
    /// </summary>
    public class QueryMovies
    {
        /// <summary>
        /// Gets or sets the genre ID to filter movies.
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Gets or sets the genre name to filter movies.
        /// </summary>
        public string? GenreName { get; set; }

        /// <summary>
        /// Gets or sets the release date to filter movies.
        /// </summary>
        public DateOnly? Release { get; set; }
    }
}
