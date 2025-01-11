namespace JCB_Cinema.Application.DTOs.Auth
{
    /// <summary>
    /// Contains constants related to identity claims and policies for authentication and authorization.
    /// </summary>
    public class IdentityData
    {
        /// <summary>
        /// The claim name used to identify an admin user.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> constant with the value "admin".
        /// </value>
        public const string AdminUserClaimName = "admin";

        /// <summary>
        /// The policy name used for admin user authorization.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> constant with the value "Admin".
        /// </value>
        public const string AdminUserPolicyName = "Admin";
    }
}
