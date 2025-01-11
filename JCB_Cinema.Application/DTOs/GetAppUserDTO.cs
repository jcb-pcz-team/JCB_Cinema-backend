namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of an application user.
    /// </summary>
    public class GetAppUserDTO
    {
        /// <summary>
        /// Gets or sets the login username of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's login.
        /// </value>
        public string? Login { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's email.
        /// </value>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's first name.
        /// </value>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's last name.
        /// </value>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the street address of the user.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's street.
        /// </value>
        public string? Street { get; set; }

        /// <summary>
        /// Gets or sets the house number of the user's address.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the user's house number.
        /// </value>
        public string? HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        /// <value>
        /// A nullable <see cref="int"/> representing the user's phone number.
        /// </value>
        public int? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the dialing code associated with the user's phone number.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the dialing code (e.g., +1, +44).
        /// </value>
        public string? DialCode { get; set; }
    }
}
