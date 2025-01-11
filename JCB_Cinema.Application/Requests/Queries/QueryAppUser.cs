namespace JCB_Cinema.Application.Requests.Queries
{
    /// <summary>
    /// Represents a query to retrieve user information based on login or email.
    /// </summary>
    public class QueryAppUser
    {
        /// <summary>
        /// Gets or sets the login of the user.
        /// This field is optional and can be used to filter users by their login.
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// This field is optional and can be used to filter users by their email.
        /// </summary>
        public string? Email { get; set; }
    }
}
