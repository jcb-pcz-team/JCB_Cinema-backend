using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.DTOs.AdminPanel;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class ScheduleService : ServiceBase, IScheduleService
    {
        private readonly IMovieProjectionService _movieProjectionService;
        private readonly IBookingTicketService _bookingTicketService;
        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService, IMovieProjectionService movieProjectionService, IBookingTicketService bookingTicketService) : base(unitOfWork, mapper, userManager, userContextService)
        {
            _movieProjectionService = movieProjectionService;
            _bookingTicketService = bookingTicketService;
        }

        public async Task<IList<GetScheduleDTO>?> Get(QuerySchedule request)
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

        public async Task<IList<AdmScheduleDTO>> GetDetailedSchedules(QuerySchedule request)
        {
            var result = new List<AdmScheduleDTO>();
            request.DateFrom = request.DateFrom ?? DateOnly.FromDateTime(DateTime.Now);
            request.DateTo = request.DateTo ?? DateOnly.FromDateTime(DateTime.Now.AddDays(7));

            // Ograniczenie liczby iteracji
            if (request.DateTo > request.DateFrom.Value.AddYears(1))
            {
                request.DateTo = request.DateFrom.Value.AddYears(1); // Ograniczamy zakres do maksymalnie 1 roku
            }

            for (var date = request.DateFrom!.Value; date <= request.DateTo!.Value; date = date.AddDays(1))
            {
                var baseRequest = new QueryMovieProjectionsCount { DateFrom = date.ToDateTime(TimeOnly.MinValue), DateTo = date.ToDateTime(TimeOnly.MaxValue) };
                var cinemaHallsRequest = _mapper.Map<QueryMovieProjectionsCount>(baseRequest);
                cinemaHallsRequest.DistinctActiveHalls = true;

                var moviesRequest = _mapper.Map<QueryMovieProjectionsCount>(baseRequest);
                moviesRequest.DistinctMovies = true;

                var tickets = new QueryBookingTicket { MovieProjectionDateFrom = date.ToDateTime(TimeOnly.MinValue), MovieProjectionDateTo = date.ToDateTime(TimeOnly.MaxValue) };

                var movieProjectionCount = await _movieProjectionService.GetCount(baseRequest);
                var dto = new AdmScheduleDTO
                {
                    Date = date,
                    MovieProjectionsCount = movieProjectionCount,
                    ActiveCinemaHalls = movieProjectionCount > 0 ? await _movieProjectionService.GetCount(cinemaHallsRequest) : 0,
                    MovieCount = movieProjectionCount > 0 ? await _movieProjectionService.GetCount(moviesRequest) : 0,
                    TotalBookingTickets = movieProjectionCount > 0 ? await _bookingTicketService.GetBookingTicketsCount(tickets) : 0,
                };
                result.Add(dto);
            }
            return result;
        }
    }
}
