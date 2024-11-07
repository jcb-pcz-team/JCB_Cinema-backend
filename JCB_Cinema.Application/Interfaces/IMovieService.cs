using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>?> Get(RequestMovies request);
    }
}
