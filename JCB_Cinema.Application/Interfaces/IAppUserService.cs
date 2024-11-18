using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IAppUserService
    {
        public Task<AppUserDTO?> GetAppUserAsync(string id);
        public Task PutAppUserAsync(RequestAppUser appUserDTO);
    }
}
