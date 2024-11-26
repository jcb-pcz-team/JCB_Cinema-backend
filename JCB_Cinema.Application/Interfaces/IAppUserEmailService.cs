using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IAppUserEmailService
    {
        public Task PutAppUserEmailAsync(RequestAppUserEmail appUserEmail);
    }
}
