using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing genre operations.
    /// </summary>
    public interface IGenreService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of genres.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IList{GetGenreDTO}}"/> representing the asynchronous operation. The result contains a list of <see cref="GetGenreDTO"/>
        /// representing all available genres.
        /// </returns>
        public Task<IList<GetGenreDTO>> Get();
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
