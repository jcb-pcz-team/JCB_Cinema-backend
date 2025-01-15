using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing cinema hall operations.
    /// </summary>
    public interface ICinemaHallService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously checks if any cinema hall exists that matches the provided criteria.
        /// </summary>
        /// <param name="predicate">
        /// An <see cref="Expression{Func{CinemaHall, bool}}"/> representing the criteria to check for matching cinema halls.
        /// </param>
        /// <returns>
        /// A <see cref="Task{bool}"/> representing the asynchronous operation. The result indicates whether any cinema hall matches the criteria.
        /// </returns>
        Task<bool> IsAny(Expression<Func<CinemaHall, bool>> predicate);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of cinema halls based on the provided query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QueryCinemaHall"/> containing the search criteria for retrieving the cinema halls.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{GetCinemaHallDTO}?}"/> representing the asynchronous operation. The result contains a list of <see cref="GetCinemaHallDTO"/>
        /// or null if no cinema halls are found based on the provided query.
        /// </returns>
        public Task<IList<GetCinemaHallDTO>?> Get(QueryCinemaHall request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a cinema hall by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// An <see cref="int"/> representing the unique identifier of the cinema hall.
        /// </param>
        /// <returns>
        /// A <see cref="Task{GetCinemaHallDTO?}"/> representing the asynchronous operation. The result contains a <see cref="GetCinemaHallDTO"/>
        /// if the cinema hall is found, or null if no cinema hall matches the given ID.
        /// </returns>
        Task<GetCinemaHallDTO?> Get(int id);
        Task<IList<Seat>> GetSeats(int cinemaHallId, bool noInclude = true);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
