using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieProjectionService
    {
        Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);
        Task<GetMovieProjectionDTO?> GetDetails(int id);
        Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionDTO);
        Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO);
        Task DeleteMovieProjection(int projectionId);
        Task<int> GetCount(QueryMovieProjectionsCount request);
    }
}
