using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing movie projection operations.
    /// </summary>
    public interface IMovieProjectionService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryMovieProjections"/> containing the search criteria for retrieving movie projections.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{GetMovieProjectionDTO}?}"/> representing the asynchronous operation. The result contains a list of 
        /// <see cref="GetMovieProjectionDTO"/> objects or null if no movie projections are found.
        /// </returns>
        Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves detailed information for a specific movie projection by its ID.
        /// </summary>
        /// <param name="id">
        /// An <see cref="int"/> representing the ID of the movie projection to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Task{GetMovieProjectionDTO?}"/> representing the asynchronous operation. The result contains a 
        /// <see cref="GetMovieProjectionDTO"/> with the projection details or null if no projection is found for the given ID.
        /// </returns>
        Task<GetMovieProjectionDTO?> GetDetails(int id);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously updates the details of an existing movie projection.
        /// </summary>
        /// <param name="projectionId">
        /// An <see cref="int"/> representing the ID of the movie projection to update.
        /// </param>
        /// <param name="movieProjectionDTO">
        /// An <see cref="UpdateMovieProjectionRequest"/> containing the updated details of the movie projection.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Asynchronously adds a new movie projection.
        /// </summary>
        /// <param name="movieProjectionDTO">
        /// An <see cref="AddMovieProjectionRequest"/> containing the details of the movie projection to be added.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Asynchronously deletes a movie projection by its ID.
        /// </summary>
        /// <param name="projectionId">
        /// An <see cref="int"/> representing the ID of the movie projection to delete.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task DeleteMovieProjection(int projectionId);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the count of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryMovieProjectionsCount"/> containing the search criteria for retrieving the count of movie projections.
        /// </param>
        /// <returns>
        /// A <see cref="Task{int}"/> representing the asynchronous operation. The result is the count of movie projections that match the query.
        /// </returns>
        Task<int> GetCount(QueryMovieProjectionsCount request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
