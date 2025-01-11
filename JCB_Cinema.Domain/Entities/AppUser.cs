using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents an application user that extends the IdentityUser class with additional properties specific to the domain.
    /// </summary>
    public class AppUser : IdentityUser
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
        /// Gets or sets the house number of the user.
        /// </summary>
        public string? HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the town or city where the user resides.
        /// </summary>
        public string? Town { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the user entity.
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the list of booking tickets associated with the user.
        /// </summary>
        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();

        /// <summary>
        /// Gets or sets the dialing code of the user's phone number. Defaults to "+48".
        /// </summary>
        public string? DialCode { get; set; } = "+48";
    }
}
