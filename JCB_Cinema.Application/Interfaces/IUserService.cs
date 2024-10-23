using JCB_Cinema.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegistrationModel model);
    }
}
