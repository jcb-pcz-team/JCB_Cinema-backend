using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces
{
    public interface ICinemaHallService
    {
        public Task<IList<GetMovieProjectionDTO>?> Get(int cinemaHallId);
    }
}
