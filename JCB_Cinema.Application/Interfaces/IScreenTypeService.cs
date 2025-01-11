using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing screen types.
    /// </summary>
    public interface IScreenTypeService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of screen types.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{IList{GetScreenTypeDTO}}"/> representing the asynchronous operation. The result contains a list of
        /// <see cref="GetScreenTypeDTO"/> objects, which represent the available screen types.
        /// </returns>
        public Task<IList<GetScreenTypeDTO>> Get();
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
