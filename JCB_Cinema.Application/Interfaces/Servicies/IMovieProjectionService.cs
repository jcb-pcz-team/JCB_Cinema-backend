using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces.Servicies
{
    public interface IMovieProjectionService
    {
        public Task<IEnumerable<GetMovieProjectionDTO>> Get(RequestMovieProjection requestMovieProjection);
    }
}
