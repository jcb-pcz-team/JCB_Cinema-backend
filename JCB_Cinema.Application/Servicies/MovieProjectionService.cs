using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
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

        public async Task AddMovieProjection(AddMovieProjectionDTO movieProjectionDTO)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                MovieProjection movie = _mapper.Map<MovieProjection>(movieProjectionDTO);
                await _unitOfWork.Repository<MovieProjection>().AddAsync(movie);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request)
        {
            var query = _unitOfWork.Repository<MovieProjection>().Queryable();

            query = query.Include(a => a.Movie)
                .Include(a => a.CinemaHall)
                .Include(a => a.Price);

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

        public async Task UpdateMovieProjection(UpdateMovieProjectionDTO movieProjectionDTO)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                MovieProjection movie = _mapper.Map<MovieProjection>(movieProjectionDTO);
                await _unitOfWork.Repository<MovieProjection>().UpdateAsync(movie);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
