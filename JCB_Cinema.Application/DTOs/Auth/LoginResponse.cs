namespace JCB_Cinema.Application.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object for the response after a successful user login.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the JWT token issued upon successful login.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the JSON Web Token (JWT).
        /// </value>
        public string JwtToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the expiration date and time of the issued JWT token.
        /// </summary>
        /// <value>
        /// A <see cref="DateTime"/> representing when the token will expire.
        /// </value>
        public DateTime ExpirationDate { get; set; }
    }
}
