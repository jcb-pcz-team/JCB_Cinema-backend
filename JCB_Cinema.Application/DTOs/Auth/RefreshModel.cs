namespace JCB_Cinema.Application.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object for refreshing an access token using a refresh token.
    /// </summary>
    public class RefreshModel
    {
        /// <summary>
        /// Gets or sets the current access token to be refreshed.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the access token.
        /// </value>
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// Gets or sets the refresh token used to generate a new access token.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the refresh token.
        /// </value>
        public string RefreshToken { get; set; } = null!;
    }
}
