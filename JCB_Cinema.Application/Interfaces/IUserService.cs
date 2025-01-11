using JCB_Cinema.Application.DTOs.Auth;
using JCB_Cinema.Application.Requests.Update;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing user-related operations, including registration, login, 
    /// role assignment, and password management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously registers a new user based on the provided registration model.
        /// </summary>
        /// <param name="model">
        /// A <see cref="RegistrationModel"/> object containing the necessary information to register a user.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IdentityResult}"/> representing the asynchronous operation. The result contains an 
        /// <see cref="IdentityResult"/> indicating the success or failure of the registration.
        /// </returns>
        Task<IdentityResult> RegisterUserAsync(RegistrationModel model);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously logs in a user based on the provided login model.
        /// </summary>
        /// <param name="model">
        /// A <see cref="LoginModel"/> object containing the user's login credentials.
        /// </param>
        /// <returns>
        /// A <see cref="Task{string}"/> representing the asynchronous operation. The result contains a JWT token 
        /// as a string if the login is successful.
        /// </returns>
        Task<string> Login(LoginModel model);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously assigns a user to a role based on the provided role assignment request.
        /// </summary>
        /// <param name="roleRequest">
        /// An <see cref="AssignUserToRoleRequest"/> object containing the user and role details.
        /// </param>
        /// <returns>
        /// A <see cref="Task{string}"/> representing the asynchronous operation. The result contains a confirmation 
        /// string upon successfully assigning the role.
        /// </returns>
        Task<string> AssignUserToRole(AssignUserToRoleRequest roleRequest);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously changes a user's password based on the provided password change details.
        /// </summary>
        /// <param name="user">
        /// A <see cref="ChangeUserPassword"/> object containing the necessary information to change the user's password.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. The result indicates whether the password 
        /// change was successful.
        /// </returns>
        Task ChangePassword(ChangeUserPassword user);
    }
}
