using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task AssignRoleToUserAsync(AppUser user, string roleName);
    }
}
