using System.ComponentModel.DataAnnotations;

namespace JCB_Cinema.Application.Auth
{
    public class RegistrationModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Role { get; set; }
    }
}
