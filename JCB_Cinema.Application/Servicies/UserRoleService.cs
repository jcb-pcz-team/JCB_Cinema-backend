using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Servicies
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRoleService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssignRoleToUserAsync(AppUser user, string roleName)
        {
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
