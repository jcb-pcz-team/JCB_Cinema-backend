using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class MovieProjectionService : ServiceBase, IMovieProjectionService
    {
        public MovieProjectionService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
        }

        public async Task<IList<GetMovieProjectionDTO>?> Get(RequestMovieProjections request)
        {
            var query = _unitOfWork.Repository<MovieProjection>().Queryable();

            query = query.Include(a => a.Movie)
                .Include(a => a.CinemaHall);

            if (!string.IsNullOrWhiteSpace(request.ScreenTypeName))
            {
                var genreValue = EnumExtensions.GetValueFromDescription<ScreenType>(request.ScreenTypeName);
                query = query.Where(m => m.ScreenType == genreValue);
            }
            if (request.CinemaHallId.HasValue)
            {
                query = query.Where(a => a.CinemaHallId == request.CinemaHallId);
            }
            var entities = await query.ToListAsync();
            return entities == null ? null : _mapper.Map<IList<GetMovieProjectionDTO>>(entities);
        }

        public async Task<GetMovieProjectionDTO?> GetDetails(int id)
        {
            var query = _unitOfWork.Repository<MovieProjection>().Queryable();
            var entity = await query.FirstOrDefaultAsync(m => m.MovieProjectionId == id);

            query.Include(a => a.Movie)
                .Include(g => g.Movie.Genre)
                .Include(s => s.ScreenType)
                .Include(c => c.CinemaHall)
                .Include(p => p.Price);

            return entity == null ? null : _mapper.Map<GetMovieProjectionDTO>(entity);
        }
    }
}
