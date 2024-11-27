using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IScheduleService
    {
        public Task<IList<GetScheduleDTO>?> Get(QuerySchedule request);
    }
}
