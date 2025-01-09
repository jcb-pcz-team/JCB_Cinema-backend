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
        Task<IList<GetMovieDTO>?> Get(QueryMovies request);
        Task<GetMovieDTO?> GetDetails(string title);
        Task<bool> IsAny(Expression<Func<Movie, bool>> predicate);
        Task<IList<GetMovieDTO>?> GetUpcoming();
        Task<string> AddMovie(AddMovieRequest movie);
        Task UpdateMovie(string title, UpdateMovieRequest movie);
        Task DeleteMovie(string title);
        Task<IList<GetMovieTitleDTO>?> GetTitles();
        Task<int?> GetMovieId(string normalizedTitle);
    }
}
