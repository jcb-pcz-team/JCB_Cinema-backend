using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Domain.Interface
{
    /// <summary>
    /// Provides methods for accessing and managing user-related context information.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Retrieves an application user based on the provided email or username.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve. Can be null or empty.</param>
        /// <param name="userName">The username of the user to retrieve. Can be null or empty.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. 
        /// The task result contains the <see cref="AppUser"/> object if found; otherwise, null.
        /// </returns>
        Task<AppUser?> GetAppUser(string? email, string? userName);

        /// <summary>
        /// Retrieves the currently authenticated application user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. 
        /// The task result contains the <see cref="AppUser"/> object representing the current user.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authenticated.</exception>
        Task<AppUser> GetAppUser();

        /// <summary>
        /// Retrieves the username of the currently authenticated user.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing the username of the current user, or null if not authenticated.
        /// </returns>
        string? GetUserName();
    }
}
