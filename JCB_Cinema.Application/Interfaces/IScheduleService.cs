using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.DTOs.AdminPanel;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing movie schedules.
    /// </summary>
    public interface IScheduleService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of schedules based on the specified query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QuerySchedule"/> object containing the search criteria for retrieving schedules.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{GetScheduleDTO}?}"/> representing the asynchronous operation. The result contains a list of
        /// <see cref="GetScheduleDTO"/> objects, or null if no schedules match the query.
        /// </returns>
        Task<IList<GetScheduleDTO>?> Get(QuerySchedule request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves detailed schedules for the admin panel based on the specified query parameters.
        /// </summary>
        /// <param name="request">
        /// A <see cref="QuerySchedule"/> object containing the search criteria for retrieving detailed schedules.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{AdmScheduleDTO}}"/> representing the asynchronous operation. The result contains a list of
        /// <see cref="AdmScheduleDTO"/> objects with detailed schedule information.
        /// </returns>
        Task<IList<AdmScheduleDTO>> GetDetailedSchedules(QuerySchedule request);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
