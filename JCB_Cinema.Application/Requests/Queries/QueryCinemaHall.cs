namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for filtering cinema halls by name.
    /// </summary>
    public class QueryCinemaHall
    {
        /// <summary>
        /// Gets or sets the name of the cinema hall to filter by.
        /// </summary>
        public string? Name { get; set; }
    }
}
