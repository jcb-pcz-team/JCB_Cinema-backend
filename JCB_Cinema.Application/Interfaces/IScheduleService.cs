using JCB_Cinema.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IScheduleService
    {
        public Task<IList<GetScheduleDTO>> GetFilteredSchedulesAsync([FromQuery]string date);
    }
}
