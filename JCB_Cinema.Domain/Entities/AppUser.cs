using Microsoft.AspNet.Identity.EntityFramework;

namespace JCB_Cinema.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string? Role { get; set; }
    }
}