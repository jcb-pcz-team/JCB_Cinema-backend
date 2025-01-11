namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to update the details of a user.
    /// </summary>
    public class PutAppUserDetails
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the street address of the user.
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// Gets or sets the house number of the user's address.
        /// </summary>
        public string? HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the dial code for the user's phone number.
        /// </summary>
        public string? DialCode { get; set; }
    }
}
