using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Domain.Interface
{
    public interface IUserContextService
    {
        Task<AppUser?> GetAppUser(string? email, string? userName);
        Task<AppUser> GetAppUser();
        string? GetUserName();
    }
}
