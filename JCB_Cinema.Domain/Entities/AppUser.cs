using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? Town { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();
    }
}