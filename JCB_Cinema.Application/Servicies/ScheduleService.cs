using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
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

        public async Task<IList<GetScheduleDTO>?> Get(RequestSchedule request)
        {
            var query = _unitOfWork.Repository<Schedule>().Queryable();
            if (request.DateFrom.HasValue)
            {
                query = query.Where(a => a.Date >= request.DateFrom);
            }
            if (request.DateTo.HasValue)
            {
                query = query.Where(a => a.Date <= request.DateTo);
            }

            var entities = await query.ToListAsync();
            return entities == null ? null : _mapper.Map<IList<GetScheduleDTO>>(entities);
        }
    }
}
