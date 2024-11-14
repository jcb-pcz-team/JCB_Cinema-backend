using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class ScheduleService : ServiceBase, IScheduleService
    {
        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
        }

        public async Task<IList<GetScheduleDTO>?> Get(RequestSchedule request)
        {
            var query = _unitOfWork.Repository<Schedule>().Queryable();
            query = query
                .Include(a => a.Screenings)
                    .ThenInclude(a => a.Movie)
                .Include(a => a.Screenings)
                    .ThenInclude(a => a.CinemaHall)
                    .ThenInclude(a => a.Seats);
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
