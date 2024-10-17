using JCB_Cinema.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.Application.Interfaces.Servicies
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>> Get([FromQuery] RequestMovies query);
    }
}
