using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string? Role { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}