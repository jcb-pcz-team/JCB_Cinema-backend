using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing user roles.
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// Asynchronously assigns a role to a user.
        /// </summary>
        /// <param name="user">
        /// An <see cref="AppUser"/> object representing the user to whom the role will be assigned.
        /// </param>
        /// <param name="roleName">
        /// A <see cref="string"/> representing the name of the role to assign to the user.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task AssignRoleToUserAsync(AppUser user, string roleName);
    }
}
