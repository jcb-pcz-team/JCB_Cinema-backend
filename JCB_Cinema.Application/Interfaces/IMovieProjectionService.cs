using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieProjectionService
    {
        public Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);
        public Task<GetMovieProjectionDTO?> GetDetails(int id);
    }
}
