using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing and accessing user context in the application.
    /// </summary>
    /// <remarks>
    /// Provides methods to retrieve information about the currently authenticated user and their associated data.
    /// </remarks>
    public class UserContextService : IUserContextService
    {
        /// <summary>
        /// Accessor for the current HTTP context.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// User manager for managing application users.
        /// </summary>
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContextService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Provides access to the current HTTP context.</param>
        /// <param name="userManager">Manages application users, including finding users by email or username.</param>
        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        /// <summary>
        /// Retrieves the username of the currently authenticated user.
        /// </summary>
        /// <returns>
        /// The username of the current user, or <c>null</c> if no user is authenticated.
        /// </returns>
        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }

        /// <summary>
        /// Retrieves a user by their email or username.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="userName">The username of the user.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="AppUser"/> object, or <c>null</c> if no user matches the criteria.
        /// </returns>
        public async Task<AppUser?> GetAppUser(string? email, string? userName)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return await _userManager.FindByEmailAsync(email);
            }
            else if (!string.IsNullOrWhiteSpace(userName))
            {
                return await _userManager.FindByNameAsync(userName);
            }
            return null;
        }

        /// <summary>
        /// Retrieves the currently authenticated user.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="AppUser"/> object.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user's username cannot be retrieved or the user does not exist in the system.
        /// </exception>
        public async Task<AppUser> GetAppUser()
        {
            var userName = GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException("The user is not authenticated.");
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("The authenticated user could not be found.");
            }
            return user;
        }
    }
}
