using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Interfaces.Servicies
{
    public interface IMovieProjectionService
    {
        public Task<IEnumerable<GetMovieProjectionDTO>> Get(string screenType);
    }
}
