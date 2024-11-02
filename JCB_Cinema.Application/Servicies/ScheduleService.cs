using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetScheduleDTO>> GetFilteredSchedulesAsync([FromQuery] string date)
        {
            var allSchedules = _unitOfWork.Repository<Schedule>().Queryable();

            DateOnly stringToDate = DateOnly.Parse(date);

            allSchedules = allSchedules.Where(x => x.Date == stringToDate);

            var allSchedulesList = await allSchedules.ToListAsync();
            return _mapper.Map<IList<GetScheduleDTO>>(allSchedulesList);
        }
    }
}
