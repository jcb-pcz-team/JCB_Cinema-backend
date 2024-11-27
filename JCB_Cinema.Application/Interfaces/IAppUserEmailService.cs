using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IAppUserEmailService
    {
        public Task PutAppUserEmailAsync(QueryAppUserEmail appUserEmail);
    }
}
