using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.DTOs.Auth
{
    /// <summary>
    /// Data Transfer Object for user registration information.
    /// </summary>
    public class RegistrationModel
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the user's username.
        /// </value>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the user's password.
        /// This field is required.
        /// </value>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the user's email address. 
        /// Must be a valid email format as per the <see cref="EmailAddress"/> attribute.
        /// </value>
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
