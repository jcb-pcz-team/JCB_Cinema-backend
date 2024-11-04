using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>?> Get(int genreId);
        public Task<IList<GetMovieDTO>?> Get(string genreName);
    }
}
