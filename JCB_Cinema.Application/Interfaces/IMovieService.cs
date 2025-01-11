using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing movie operations.
    /// </summary>
    public interface IMovieService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of movies based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryMovies"/> containing the search criteria for retrieving movies.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{GetMovieDTO}?}"/> representing the asynchronous operation. The result contains a list of 
        /// <see cref="GetMovieDTO"/> objects or null if no movies are found.
        /// </returns>
        Task<IList<GetMovieDTO>?> Get(QueryMovies request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves detailed information for a specific movie by its title.
        /// </summary>
        /// <param name="title">
        /// A <see cref="string"/> representing the title of the movie to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Task{GetMovieDTO?}"/> representing the asynchronous operation. The result contains a 
        /// <see cref="GetMovieDTO"/> with the movie details or null if no movie is found with the specified title.
        /// </returns>
        Task<GetMovieDTO?> GetDetails(string title);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously checks if any movie exists that matches the provided predicate.
        /// </summary>
        /// <param name="predicate">
        /// A <see cref="Expression{Func{Movie, bool}}"/> representing the condition to check for existing movies.
        /// </param>
        /// <returns>
        /// A <see cref="Task{bool}"/> representing the asynchronous operation. The result is true if a movie matches the predicate, false otherwise.
        /// </returns>
        Task<bool> IsAny(Expression<Func<Movie, bool>> predicate);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of upcoming movies.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IList{GetMovieDTO}?}"/> representing the asynchronous operation. The result contains a list of
        /// <see cref="GetMovieDTO"/> objects representing the upcoming movies, or null if no upcoming movies are found.
        /// </returns>
        Task<IList<GetMovieDTO>?> GetUpcoming();
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously adds a new movie.
        /// </summary>
        /// <param name="movie">
        /// An <see cref="AddMovieRequest"/> containing the details of the movie to be added.
        /// </param>
        /// <returns>
        /// A <see cref="Task{string}"/> representing the asynchronous operation. The result is a string indicating the result of the operation.
        /// </returns>
        Task<string> AddMovie(AddMovieRequest movie);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously updates the details of an existing movie.
        /// </summary>
        /// <param name="title">
        /// A <see cref="string"/> representing the title of the movie to update.
        /// </param>
        /// <param name="movie">
        /// An <see cref="UpdateMovieRequest"/> containing the updated details of the movie.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task UpdateMovie(string title, UpdateMovieRequest movie);

        /// <summary>
        /// Asynchronously deletes a movie by its title.
        /// </summary>
        /// <param name="title">
        /// A <see cref="string"/> representing the title of the movie to delete.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task DeleteMovie(string title);


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of movie titles.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IList{GetMovieTitleDTO}?}"/> representing the asynchronous operation. The result contains a list of 
        /// <see cref="GetMovieTitleDTO"/> objects representing the movie titles, or null if no titles are found.
        /// </returns>
        Task<IList<GetMovieTitleDTO>?> GetTitles();
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves the movie ID for a movie with the specified normalized title.
        /// </summary>
        /// <param name="normalizedTitle">
        /// A <see cref="string"/> representing the normalized title of the movie to retrieve the ID for.
        /// </param>
        /// <returns>
        /// A <see cref="Task{int?}"/> representing the asynchronous operation. The result is the ID of the movie, or null if no movie is found.
        /// </returns>
        Task<int?> GetMovieId(string normalizedTitle);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
