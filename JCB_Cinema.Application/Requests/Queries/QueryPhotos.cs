namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query for filtering photos based on various criteria such as ID and creation date.
    /// </summary>
    public class QueryPhotos
    {
        /// <summary>
        /// Gets or sets the photo ID to filter the photos.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the start date for filtering photos created from a specific date.
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Gets or sets the end date for filtering photos created until a specific date.
        /// </summary>
        public DateTime? CreatedTo { get; set; }
    }
}
