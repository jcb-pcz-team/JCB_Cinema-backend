using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>?> Get(QueryMovies request);
        public Task<GetMovieDTO?> GetDetails(string title);
        Task<bool> IsAny(Expression<Func<Movie, bool>> predicate);
        public Task<IList<GetMovieDTO>?> GetUpcoming();
    }
}
