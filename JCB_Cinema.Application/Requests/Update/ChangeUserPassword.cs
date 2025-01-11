namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to change a user's password.
    /// </summary>
    public class ChangeUserPassword
    {
        /// <summary>
        /// Gets or sets the email of the user whose password is to be changed.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the username of the user whose password is to be changed.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the current password of the user.
        /// </summary>
        public string OldPassword { get; set; } = null!;

        /// <summary>
        /// Gets or sets the new password for the user.
        /// </summary>
        public string NewPassword { get; set; } = null!;
    }
}
