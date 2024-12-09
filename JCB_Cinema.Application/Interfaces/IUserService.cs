using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegistrationModel model);
        Task<string> Login(LoginModel model);
        Task<string> AssignUserToRole(AssignUserToRoleRequest roleRequest);
        Task ChangePassword(ChangeUserPassword user);
    }
}
