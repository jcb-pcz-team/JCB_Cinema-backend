using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the movie projection service.
    /// Provides methods for retrieving, adding, updating, and deleting movie projections.
    /// </summary>
    public interface IMovieProjectionService
    {
        /// <summary>
        /// Retrieves a list of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">The request containing query parameters for retrieving movie projections.</param>
        /// <returns>A list of <see cref="GetMovieProjectionDTO"/> containing the details of the movie projections.</returns>
        Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);

        /// <summary>
        /// Retrieves the details of a specific movie projection.
        /// </summary>
        /// <param name="id">The ID of the movie projection to retrieve.</param>
        /// <returns>The details of the movie projection as a <see cref="GetMovieProjectionDTO"/>.</returns>
        Task<GetMovieProjectionDTO?> GetDetails(int id);

        /// <summary>
        /// Updates an existing movie projection.
        /// </summary>
        /// <param name="projectionId">The ID of the movie projection to update.</param>
        /// <param name="movieProjectionDTO">The request containing the updated movie projection data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Adds a new movie projection.
        /// </summary>
        /// <param name="movieProjectionDTO">The request containing the data for the new movie projection.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Deletes a specific movie projection.
        /// </summary>
        /// <param name="projectionId">The ID of the movie projection to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteMovieProjection(int projectionId);

        /// <summary>
        /// Retrieves the count of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">The request containing the query parameters for counting movie projections.</param>
        /// <returns>The count of movie projections matching the query parameters.</returns>
        Task<int> GetCount(QueryMovieProjectionsCount request);

        /// <summary>
        /// Checks if a specific seat is reserved for a given movie projection.
        /// </summary>
        /// <param name="movieProjectionId">The ID of the movie projection to check for seat reservation.</param>
        /// <param name="seatId">The ID of the seat to check for reservation status.</param>
        /// <returns>True if the seat is reserved, otherwise false.</returns>
        Task<bool> IsSeatReserved(int movieProjectionId, int seatId);

        /// <summary>
        /// Retrieves the status of seats for a given movie projection.
        /// </summary>
        /// <param name="movieProjectionId">The ID of the movie projection to retrieve seat statuses for.</param>
        /// <returns>A list of <see cref="SeatDTO"/> containing the status of the seats.</returns>
        Task<IList<SeatDTO>> SeatsStatus(int movieProjectionId);
    }
}
