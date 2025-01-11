namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for filtering movie schedules based on a date range.
    /// </summary>
    public class QuerySchedule
    {
        /// <summary>
        /// Gets or sets the start date for filtering schedules.
        /// </summary>
        public DateOnly? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the end date for filtering schedules.
        /// </summary>
        public DateOnly? DateTo { get; set; }
    }
}
