namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for booking tickets with optional filtering by movie projection date range.
    /// </summary>
    public class QueryBookingTicket
    {
        /// <summary>
        /// Gets or sets the start date for filtering movie projections by date.
        /// </summary>
        public DateTime? MovieProjectionDateFrom { get; set; }

        /// <summary>
        /// Gets or sets the end date for filtering movie projections by date.
        /// </summary>
        public DateTime? MovieProjectionDateTo { get; set; }
    }
}
