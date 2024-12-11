using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>?> Get(QueryMovies request);
        public Task<GetMovieDTO?> GetDetails(string title);
        public Task<bool> IsAny(Expression<Func<Movie, bool>> predicate);
        public Task<IList<GetMovieDTO>?> GetUpcoming();
        public Task<string> AddMovie(AddMovieDTO movie);
        public Task UpdateMovie(UpdateMovieDTO movie);
        Task DeleteMovie(int id);
    }
}
