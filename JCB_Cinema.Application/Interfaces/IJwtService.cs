using JCB_Cinema.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing JWT (JSON Web Token) operations.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Asynchronously generates a JWT for the specified user.
        /// </summary>
        /// <param name="user">
        /// An <see cref="AppUser"/> representing the user for whom the JWT will be generated.
        /// </param>
        /// <returns>
        /// A <see cref="Task{JwtSecurityToken}"/> representing the asynchronous operation. The result is a <see cref="JwtSecurityToken"/> 
        /// containing the generated token for the user.
        /// </returns>
        public Task<JwtSecurityToken> GenerateJwtAsync(AppUser user);

        /// <summary>
        /// Serializes the provided JWT security token into a string representation.
        /// </summary>
        /// <param name="token">
        /// A <see cref="JwtSecurityToken"/> that needs to be serialized into a string.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> representing the serialized JWT token.
        /// </returns>
        public string WriteToken(JwtSecurityToken token);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
        /// <summary>
        /// Validates the provided JWT token and returns the associated claims principal.
        /// </summary>
        /// <param name="token">
        /// A <see cref="string"/> representing the JWT token to be validated.
        /// </param>
        /// <returns>
        /// A <see cref="ClaimsPrincipal?"/> representing the claims of the user if the token is valid. 
        /// Returns null if the token is invalid or cannot be parsed.
        /// </returns>
        public ClaimsPrincipal? ValidateJwt(string token);
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
