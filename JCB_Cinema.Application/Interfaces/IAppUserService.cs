using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IAppUserService
    {
        public Task<GetAppUserDTO?> GetAppUserAsync(QueryAppUser request);
        public Task PutAppUserAsync(PutAppUserDetails appUserDTO);
    }
}
