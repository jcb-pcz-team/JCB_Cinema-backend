using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing application user data and operations.
    /// </summary>
    public interface IAppUserService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves an application user based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryAppUser"/> containing the search criteria for retrieving the application user.
        /// </param>
        /// <returns>
        /// A <see cref="Task{GetAppUserDTO?}"/> representing the asynchronous operation. The result contains the user details
        /// in a <see cref="GetAppUserDTO"/> or null if no user is found.
        /// </returns>
        public Task<GetAppUserDTO?> GetAppUserAsync(QueryAppUser request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously updates the details of an existing application user.
        /// </summary>
        /// <param name="appUserDTO">
        /// A <see cref="PutAppUserDetails"/> containing the updated user details.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous update operation.
        /// </returns>
        public Task PutAppUserAsync(PutAppUserDetails appUserDTO);

        /// <summary>
        /// Asynchronously updates the email address of an existing application user.
        /// </summary>
        /// <param name="appUserEmail">
        /// A <see cref="QueryAppUserEmail"/> containing the user's current and new email addresses.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous update operation.
        /// </returns>
        public Task PutAppUserEmailAsync(QueryAppUserEmail appUserEmail);
    }
}
