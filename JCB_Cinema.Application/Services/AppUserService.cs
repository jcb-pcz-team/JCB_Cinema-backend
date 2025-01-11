using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Application.Services;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class that provides operations related to app users, including 
    /// retrieving user details, updating user information, and updating the user's email.
    /// </summary>
    public class AppUserService : ServiceBase, IAppUserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data objects.</param>
        /// <param name="userManager">The user manager for managing user-related operations.</param>
        /// <param name="userContextService">The service that provides the current user's context.</param>
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService)
            : base(unitOfWork, mapper, userManager, userContextService) { }

        /// <summary>
        /// Retrieves details of an app user based on the specified request parameters.
        /// If the current user is not an admin, only their own details are returned.
        /// </summary>
        /// <param name="request">The query request containing the login or email to filter users.</param>
        /// <returns>A <see cref="GetAppUserDTO"/> containing the user's details, or null if no user is found.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user does not have permission to access the data.</exception>
        public async Task<GetAppUserDTO?> GetAppUserAsync(QueryAppUser request)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException("Brak uprawnień do wykonania tej operacji.");

            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                // the user is not admin, so he can't get other users details
                return _mapper.Map<GetAppUserDTO>(currentUser);
            }

            AppUser? user = null;
            if (!string.IsNullOrEmpty(request.Login))
            {
                user = await _userManager.FindByNameAsync(request.Login);
            }
            else if (!string.IsNullOrEmpty(request.Email))
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            }
            else
            {
                // if every RequestAppUser property is null, that's mean Admin wants own User details
                return _mapper.Map<GetAppUserDTO>(currentUser);
            }

            return _mapper.Map<GetAppUserDTO>(user);
        }

        /// <summary>
        /// Updates the details of the current app user.
        /// </summary>
        /// <param name="appUserRequest">The request containing the updated user details.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not authorized to perform the operation.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the update operation fails.</exception>
        public async Task PutAppUserAsync(PutAppUserDetails appUserRequest)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            _mapper.Map(appUserRequest, currentUser);
            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Updates the email of the current app user.
        /// </summary>
        /// <param name="appUserEmail">The request containing the new email details.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the current user is not authorized to perform the operation.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the update operation fails.</exception>
        public async Task PutAppUserEmailAsync(QueryAppUserEmail appUserEmail)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            _mapper.Map(appUserEmail, currentUser);
            var updateResult = await _userManager.UpdateAsync(currentUser);

            if (!updateResult.Succeeded)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
