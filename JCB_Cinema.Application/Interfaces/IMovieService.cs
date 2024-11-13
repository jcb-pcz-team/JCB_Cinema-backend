using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        public Task<IList<GetMovieDTO>?> Get(RequestMovies request);
        public Task<GetMovieDTO?> GetDetails(int id);
        Task<bool> IsAny(Expression<Func<Movie, bool>> predicate);
        public Task<IList<GetMovieDTO>?> GetUpcoming();
    }
}
