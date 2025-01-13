using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Application.Services;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class for managing movie projections. Provides methods to add, retrieve, update, and delete movie projections.
    /// </summary>
    public class MovieProjectionService : ServiceBase, IMovieProjectionService
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieProjectionService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work instance for data repository operations.</param>
        /// <param name="mapper">Mapper instance for object mapping between entities and DTOs.</param>
        /// <param name="userManager">UserManager instance for managing user-related operations.</param>
        /// <param name="userContextService">UserContextService instance for user-specific context.</param>
        /// <param name="movieService">MovieService instance for managing movie-related operations.</param>
        public MovieProjectionService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService, IMovieService movieService) : base(unitOfWork, mapper, userManager, userContextService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Adds a new movie projection.
        /// </summary>
        /// <param name="movieProjectionDTO">Request object containing the details of the movie projection to add.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when the movie or cinema hall is not found.</exception>
        public async Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO)
        {
            movieProjectionDTO.MovieNormalizedTitle = movieProjectionDTO.MovieNormalizedTitle.NormalizeString();
            MovieProjection movieProjection = _mapper.Map<MovieProjection>(movieProjectionDTO);

            Movie? movie = await _unitOfWork.Repository<Movie>().Queryable().FirstOrDefaultAsync(x => x.NormalizedTitle == movieProjectionDTO.MovieNormalizedTitle);
            if (movie != null)
            {
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

        /// <summary>
        /// Retrieves a list of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">Request object containing the query parameters for movie projections.</param>
        /// <returns>List of <see cref="GetMovieProjectionDTO"/> containing the details of the movie projections.</returns>
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

        /// <summary>
        /// Retrieves the details of a specific movie projection.
        /// </summary>
        /// <param name="id">ID of the movie projection.</param>
        /// <returns>Movie projection details as a <see cref="GetMovieProjectionDTO"/>.</returns>
        public async Task<GetMovieProjectionDTO?> GetDetails(int id)
        {
            var query = _unitOfWork.Repository<MovieProjection>().Queryable();

            var entity = await query.Include(a => a.Movie)
                .Include(c => c.CinemaHall)
                .Include(p => p.Price)
                .FirstOrDefaultAsync(m => m.MovieProjectionId == id);

            return entity == null ? null : _mapper.Map<GetMovieProjectionDTO>(entity);
        }

        /// <summary>
        /// Updates an existing movie projection.
        /// </summary>
        /// <param name="projectionId">ID of the movie projection to update.</param>
        /// <param name="movieProjectionRequest">Request object containing the updated details of the movie projection.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user does not have the necessary permissions to update the movie projection.</exception>
        /// <exception cref="NullReferenceException">Thrown when the movie projection or movie does not exist.</exception>
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
                throw new NullReferenceException("Movie projection does not exist.");
            }

            var movieId = await _movieService.GetMovieId(movieProjectionRequest.NormalizedTitle);
            if (!movieId.HasValue)
            {
                throw new NullReferenceException("Movie does not exist.");
            }

            _mapper.Map(movieProjectionRequest, proj);
            proj.MovieId = movieId.Value;

            await _unitOfWork.Repository<MovieProjection>().UpdateAsync(proj);
        }

        /// <summary>
        /// Deletes a specific movie projection.
        /// </summary>
        /// <param name="projectionId">ID of the movie projection to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteMovieProjection(int projectionId)
        {
            await _unitOfWork.Repository<MovieProjection>().DeleteAsync(projectionId);
        }

        /// <summary>
        /// Retrieves the count of movie projections based on the provided query parameters.
        /// </summary>
        /// <param name="request">Request object containing the query parameters for counting movie projections.</param>
        /// <returns>Count of movie projections matching the query parameters.</returns>
        public async Task<int> GetCount(QueryMovieProjectionsCount request)
        {
            var query = _unitOfWork.Repository<MovieProjection>().Queryable();

            if (!string.IsNullOrWhiteSpace(request.ScreenTypeName))
            {
                var genreValue = EnumExtensions.GetValueFromDescription<ScreenType>(request.ScreenTypeName);
                query = query.Where(m => m.ScreenType == genreValue);
            }
            if (request.CinemaHallId.HasValue)
            {
                query = query.Where(a => a.CinemaHallId == request.CinemaHallId);
            }
            if (!string.IsNullOrEmpty(request.MovieName))
            {
                query = query.Where(a => a.Movie != null && a.Movie.NormalizedTitle == request.MovieName);
            }
            if (request.DateFrom.HasValue)
            {
                query = query.Where(a => a.ScreeningTime >= request.DateFrom);
            }
            if (request.DateTo.HasValue)
            {
                query = query.Where(a => a.ScreeningTime <= request.DateTo);
            }

            // distinct
            if (request.DistinctActiveHalls.HasValue)
            {
                return await query.Select(a => a.CinemaHallId).Distinct().CountAsync();
            }
            if (request.DistinctMovies.HasValue)
            {
                return await query.Where(a => !string.IsNullOrEmpty(a.Movie.NormalizedTitle)).Select(a => a.Movie.NormalizedTitle).Distinct().CountAsync();
            }
            return await query.CountAsync();
        }

        /// <summary>
        /// Checks if a seat is reserved for a specific movie projection.
        /// </summary>
        /// <param name="movieProjectionId">ID of the movie projection.</param>
        /// <param name="seatId">ID of the seat to check for reservation.</param>
        /// <returns>True if the seat is reserved, otherwise false.</returns>
        public async Task<bool> IsSeatReserved(int movieProjectionId, int seatId)
        {
            return await _unitOfWork.Repository<BookingTicket>()
                .Queryable()
                .AnyAsync(a => a.MovieProjectionId == movieProjectionId && a.SeatId == seatId
                    && ((a.ExpiresAt.HasValue && a.ExpiresAt > DateTime.Now)
                    || a.IsConfirmed));
        }
    }
}
