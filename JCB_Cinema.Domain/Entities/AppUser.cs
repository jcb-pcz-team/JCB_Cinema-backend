using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();
    }
}