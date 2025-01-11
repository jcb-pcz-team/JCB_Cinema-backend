namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for counting movie projections based on various filtering criteria.
    /// </summary>
    public class QueryMovieProjectionsCount
    {
        /// <summary>
        /// Gets or sets the start date to filter movie projections.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the end date to filter movie projections.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Gets or sets the screen type name to filter movie projections.
        /// </summary>
        public string? ScreenTypeName { get; set; }

        /// <summary>
        /// Gets or sets the cinema hall ID to filter movie projections.
        /// </summary>
        public int? CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the movie name to filter movie projections.
        /// </summary>
        public string? MovieName { get; set; }

        /// <summary>
        /// Gets or sets a flag to indicate if only distinct active cinema halls should be counted.
        /// </summary>
        public bool? DistinctActiveHalls { get; set; }

        /// <summary>
        /// Gets or sets a flag to indicate if only distinct movies should be counted.
        /// </summary>
        public bool? DistinctMovies { get; set; }
    }
}
