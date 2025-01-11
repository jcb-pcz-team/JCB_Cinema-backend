namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details for updating a user's email address.
    /// </summary>
    public class GetAppUserEmailDTO
    {
        /// <summary>
        /// Gets or sets the current email address of the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the current email address of the user.
        /// </value>
        public string CurrentEmail { get; set; } = null!;

        /// <summary>
        /// Gets the new email address to be assigned to the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the new email address. This is a read-only property.
        /// </value>
        public string NewEmail { get; } = string.Empty;
    }
}
