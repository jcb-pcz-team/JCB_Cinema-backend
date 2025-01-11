namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query to change the user's email.
    /// </summary>
    public class QueryAppUserEmail
    {
        /// <summary>
        /// Gets or sets the current email address of the user.
        /// </summary>
        public string CurrentEmail { get; set; } = null!;

        /// <summary>
        /// Gets or sets the new email address to be assigned to the user.
        /// </summary>
        public string NewEmail { get; set; } = null!;
    }
}
