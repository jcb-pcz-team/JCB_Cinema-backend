using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieProjectionService
    {
        public Task<IList<GetMovieProjectionDTO>?> Get(RequestMovieProjections request);
        public Task<GetMovieProjectionDTO?> GetDetails(int id);
    }
}
