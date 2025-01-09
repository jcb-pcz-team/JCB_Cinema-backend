using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.DTOs.AdminPanel;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IScheduleService
    {
        public Task<IList<GetScheduleDTO>?> Get(QuerySchedule request);
        Task<IList<AdmScheduleDTO>> GetDetailedSchedules(QuerySchedule request);
    }
}
