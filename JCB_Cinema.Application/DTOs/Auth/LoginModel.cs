using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object for user login information.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's email address. 
        /// Must be a valid email format.
        /// </value>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's username.
        /// </value>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the user's password.
        /// This field is required.
        /// </value>
        public string Password { get; set; } = null!;
    }
}
