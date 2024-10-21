using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces.Servicies
{
    public interface IGenreService
    {
        public Task<IList<GetGenreDTO>> Get();
    }
}
