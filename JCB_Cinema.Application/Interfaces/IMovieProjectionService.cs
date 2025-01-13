using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for managing operations related to movie projections.
    /// </summary>
    public interface IMovieProjectionService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryMovieProjections"/> containing the search criteria for movie projections.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{GetMovieProjectionDTO}?}"/> representing the asynchronous operation.
        /// The result contains a list of <see cref="GetMovieProjectionDTO"/> representing the movie projections.
        /// </returns>
        Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves detailed information about a specific movie projection.
        /// </summary>
        /// <param name="id">
        /// An <see cref="int"/> representing the unique identifier of the movie projection.
        /// </param>
        /// <returns>
        /// A <see cref="Task{GetMovieProjectionDTO?}"/> representing the asynchronous operation.
        /// The result contains a <see cref="GetMovieProjectionDTO"/> with detailed information about the projection.
        /// </returns>
        Task<GetMovieProjectionDTO?> GetDetails(int id);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously updates the details of an existing movie projection.
        /// </summary>
        /// <param name="projectionId">
        /// An <see cref="int"/> representing the unique identifier of the movie projection to be updated.
        /// </param>
        /// <param name="movieProjectionDTO">
        /// A <see cref="UpdateMovieProjectionRequest"/> containing the updated details for the movie projection.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous update operation.
        /// </returns>
        Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Asynchronously adds a new movie projection.
        /// </summary>
        /// <param name="movieProjectionDTO">
        /// A <see cref="AddMovieProjectionRequest"/> containing the details of the new movie projection.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous add operation.
        /// </returns>
        Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO);

        /// <summary>
        /// Asynchronously deletes a movie projection based on its unique identifier.
        /// </summary>
        /// <param name="projectionId">
        /// An <see cref="int"/> representing the unique identifier of the movie projection to be deleted.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous delete operation.
        /// </returns>
        Task DeleteMovieProjection(int projectionId);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the total count of movie projections that match the provided query criteria.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryMovieProjectionsCount"/> containing the search criteria for counting the movie projections.
        /// </param>
        /// <returns>
        /// A <see cref="Task{int}"/> representing the asynchronous operation.
        /// The result contains the total count of movie projections that match the criteria.
        /// </returns>
        Task<int> GetCount(QueryMovieProjectionsCount request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously checks if a specific seat is reserved for a movie projection.
        /// </summary>
        /// <param name="movieProjectionId">
        /// An <see cref="int"/> representing the unique identifier of the movie projection.
        /// </param>
        /// <param name="seatId">
        /// An <see cref="int"/> representing the unique identifier of the seat to check.
        /// </param>
        /// <returns>
        /// A <see cref="Task{bool}"/> representing the asynchronous operation.
        /// The result indicates whether the seat is reserved (<c>true</c>) or not (<c>false</c>).
        /// </returns>
        Task<bool> IsSeatReserved(int movieProjectionId, int seatId);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
