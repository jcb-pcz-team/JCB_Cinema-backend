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
        private readonly IMovieService _movieService;
        public MovieProjectionService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService, IMovieService movieService) : base(unitOfWork, mapper, userManager, userContextService)
        {
            _movieService = movieService;
        }
        public async Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO)
        {
            movieProjectionDTO.MovieNormalizedTitle = movieProjectionDTO.MovieNormalizedTitle.NormalizeString();
            MovieProjection movieProjection = _mapper.Map<MovieProjection>(movieProjectionDTO);

            Movie? movie = await _unitOfWork.Repository<Movie>().Queryable().FirstOrDefaultAsync(x => x.NormalizedTitle == movieProjectionDTO.MovieNormalizedTitle);
            if (movie != null) {
                movieProjection.MovieId = movie.MovieId;
            }
            else
            {
                throw new ArgumentException("Movie Not Found");
            }

            CinemaHall? cinemaHall = await _unitOfWork.Repository<CinemaHall>().Queryable().FirstOrDefaultAsync(c => c.CinemaHallId == movieProjectionDTO.CinemaHallId);
            if (cinemaHall == null)
            {
                throw new ArgumentException("Cinema Hall Not Found");
            }

            await _unitOfWork.Repository<MovieProjection>().AddAsync(movieProjection);
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

            var entity = await query.Include(a => a.Movie)
                .Include(p => p.Price)
                .FirstOrDefaultAsync(m => m.MovieProjectionId == id);

            return entity == null ? null : _mapper.Map<GetMovieProjectionDTO>(entity);
        }

        public async Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionRequest)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null || !await _userManager.IsInRoleAsync(currentUser, "Admin"))
                throw new UnauthorizedAccessException();

            var proj = await _unitOfWork.Repository<MovieProjection>().Queryable()
                .Include(a => a.Price)
                .FirstOrDefaultAsync(a => projectionId == a.MovieProjectionId);
            if (proj == null)
            {
                throw new NullReferenceException("Movie projection does not exists.");
            }
            var movieId = await _movieService.GetMovieId(movieProjectionRequest.NormalizedTitle);
            if (!movieId.HasValue)
            {
                throw new NullReferenceException("Movie does not exists.");
            }

            _mapper.Map(movieProjectionRequest, proj);
            proj.MovieId = movieId.Value;
            await _unitOfWork.Repository<MovieProjection>().UpdateAsync(proj);
        }

        public Task DeleteMovieProjection(int projectionId)
        {
            throw new NotImplementedException();
        }
    }
}
