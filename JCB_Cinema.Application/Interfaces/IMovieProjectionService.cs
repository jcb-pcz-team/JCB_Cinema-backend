using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieProjectionService
    {
        public Task<IList<GetMovieProjectionDTO>?> Get(string screenType);
    }
}
