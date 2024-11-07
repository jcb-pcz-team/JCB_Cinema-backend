using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IScheduleService
    {
        public Task<IList<GetScheduleDTO>?> Get(RequestSchedule request);
    }
}
