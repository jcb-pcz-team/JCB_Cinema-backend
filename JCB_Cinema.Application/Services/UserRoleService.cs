using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing user roles within the application.
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        /// <summary>
        /// User manager for managing application users.
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleService"/> class.
        /// </summary>
        /// <param name="userManager">Manages application users and their roles.</param>
        public UserRoleService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Assigns a specified role to a user asynchronously.
        /// </summary>
        /// <param name="user">The user to whom the role will be assigned.</param>
        /// <param name="roleName">The name of the role to assign.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if assigning the role to the user fails.
        /// </exception>
        public async Task AssignRoleToUserAsync(AppUser user, string roleName)
        {
            // Check if the user is already in the role
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                // Add user to the role
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Failed to assign role to user.");
                }
            }
        }
    }
}
