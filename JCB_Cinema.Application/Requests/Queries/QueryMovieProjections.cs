namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for filtering movie projections based on screen type and cinema hall.
    /// </summary>
    public class QueryMovieProjections
    {
        /// <summary>
        /// Gets or sets the name of the screen type to filter by.
        /// </summary>
        public string? ScreenTypeName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cinema hall to filter by.
        /// </summary>
        public int? CinemaHallId { get; set; }
    }
}
