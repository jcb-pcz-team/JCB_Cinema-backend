namespace JCB_Cinema.Application.Requests.Update
{
    /// <summary>
    /// Represents a request to assign a user to a specific role.
    /// </summary>
    public class AssignUserToRoleRequest
    {
        /// <summary>
        /// Gets or sets the username of the user to be assigned a role.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the role to be assigned to the user.
        /// </summary>
        public string Role { get; set; } = null!;
    }
}
