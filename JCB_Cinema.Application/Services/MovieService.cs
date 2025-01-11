using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Application.Servicies;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing movies within the cinema application.
    /// </summary>
    /// <remarks>
    /// Provides methods to add, delete, update, and retrieve movies. This service ensures
    /// that only authorized users can perform administrative operations.
    /// </remarks>
    public class MovieService : ServiceBase, IMovieService
    {
        private readonly IPhotoService _photoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieService"/> class.
        /// </summary>
        /// <param name="photoService">Service for managing movie photos.</param>
        /// <param name="unitOfWork">Unit of work for handling database transactions.</param>
        /// <param name="mapper">Mapper for transforming objects between DTOs and domain models.</param>
        /// <param name="userManager">Manager for user authentication and roles.</param>
        /// <param name="userContextService">Service to retrieve the current user's context.</param>
        public MovieService(
            IPhotoService photoService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IUserContextService userContextService
        ) : base(unitOfWork, mapper, userManager, userContextService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Adds a new movie to the database.
        /// </summary>
        /// <param name="addMovie">The request containing movie details.</param>
        /// <returns>The normalized title of the added movie.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authorized.</exception>
        /// <exception cref="NullReferenceException">Thrown if the movie's photo does not exist.</exception>
        public async Task<string> AddMovie(AddMovieRequest addMovie)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
                throw new UnauthorizedAccessException();

            Movie movie = _mapper.Map<Movie>(addMovie);

            var photo = await _photoService.Get(movie.Title);
            if (photo == null)
                throw new NullReferenceException("Photo does not exist.");
            movie.PhotoId = photo.Id;

            await _unitOfWork.Repository<Movie>().AddAsync(movie);
            return movie.NormalizedTitle;
        }

        /// <summary>
        /// Deletes a movie from the database.
        /// </summary>
        /// <param name="title">The normalized title of the movie to delete.</param>
        /// <exception cref="NullReferenceException">Thrown if the movie does not exist.</exception>
        public async Task DeleteMovie(string title)
        {
            var delEntity = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Photo)
                .Where(a => a.NormalizedTitle == title)
                .Select(a => new
                {
                    Id = a.MovieId,
                    PosterId = a.Photo != null ? a.Photo.Id : 0
                })
                .FirstOrDefaultAsync();

            if (delEntity == null)
                throw new NullReferenceException("Movie does not exist.");
            if (delEntity.PosterId > 0)
            {
                await _unitOfWork.Repository<Photo>().DeleteAsync(delEntity.PosterId);
            }
            await _unitOfWork.Repository<Movie>().DeleteAsync(delEntity.Id);
        }

        /// <summary>
        /// Retrieves a list of movies based on the given query parameters.
        /// </summary>
        /// <param name="request">The query parameters for filtering movies.</param>
        /// <returns>A list of movies matching the query, or null if no movies match.</returns>
        public async Task<IList<GetMovieDTO>?> Get(QueryMovies request)
        {
            var query = _unitOfWork.Repository<Movie>().Queryable();
            query = query.Include(a => a.Photo);

            if (request.GenreId.HasValue)
            {
                query = query.Where(m => (int?)m.Genre == request.GenreId);
            }
            else if (!string.IsNullOrWhiteSpace(request.GenreName))
            {
                Genre? genreValue = EnumExtensions.GetValueFromDescription<Genre>(request.GenreName);
                query = query.Where(m => m.Genre == genreValue);
            }
            if (request.Release.HasValue)
            {
                query = query.Where(a => a.ReleaseDate == request.Release);
            }

            var moviesList = await query.ToListAsync();
            return moviesList == null ? null : _mapper.Map<IList<GetMovieDTO>>(moviesList);
        }

        /// <summary>
        /// Retrieves detailed information for a specific movie.
        /// </summary>
        /// <param name="title">The normalized title of the movie.</param>
        /// <returns>The detailed information of the movie, or null if the movie does not exist.</returns>
        public async Task<GetMovieDTO?> GetDetails(string title)
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Photo)
                .FirstOrDefaultAsync(m => m.NormalizedTitle == title);
            return query == null ? null : _mapper.Map<GetMovieDTO>(query);
        }

        /// <summary>
        /// Retrieves a list of all movie titles.
        /// </summary>
        /// <returns>A list of movie titles, or null if no movies exist.</returns>
        public async Task<IList<GetMovieTitleDTO>?> GetTitles()
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable().ToListAsync();
            return query == null ? null : _mapper.Map<IList<GetMovieTitleDTO>?>(query);
        }

        /// <summary>
        /// Retrieves a list of upcoming movies.
        /// </summary>
        /// <returns>A list of movies with release dates in the future, or null if no upcoming movies exist.</returns>
        public async Task<IList<GetMovieDTO>?> GetUpcoming()
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Photo)
                .Where(m => m.ReleaseDate > DateOnly.FromDateTime(DateTime.UtcNow))
                .ToListAsync();
            return query == null ? null : _mapper.Map<IList<GetMovieDTO>>(query);
        }

        /// <summary>
        /// Checks if any movie matches the given condition.
        /// </summary>
        /// <param name="predicate">An expression defining the condition to check.</param>
        /// <returns>True if a movie matches the condition; otherwise, false.</returns>
        public async Task<bool> IsAny(Expression<Func<Movie, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Photo)
                .AnyAsync(predicate);
            return entity;
        }

        /// <summary>
        /// Updates the details of an existing movie.
        /// </summary>
        /// <param name="title">The normalized title of the movie to update.</param>
        /// <param name="updateMovie">The updated movie details.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authorized.</exception>
        /// <exception cref="NullReferenceException">Thrown if the movie does not exist.</exception>
        public async Task UpdateMovie(string title, UpdateMovieRequest updateMovie)
        {
            var currentUserName = _userContextService.GetUserName();
            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
                throw new UnauthorizedAccessException();

            var existingMovie = await _unitOfWork.Repository<Movie>()
                .Queryable()
                .FirstOrDefaultAsync(m => m.NormalizedTitle == title);

            if (existingMovie == null)
                throw new NullReferenceException();

            var movie = _mapper.Map(updateMovie, existingMovie);

            if (updateMovie.SetPreviousPoster.HasValue && !updateMovie.SetPreviousPoster.Value)
            {
                var photo = await _photoService.Get(updateMovie.Title);
                movie.PhotoId = photo?.Id;
            }

            await _unitOfWork.Repository<Movie>().UpdateAsync(movie);
        }

        /// <summary>
        /// Retrieves the ID of a movie by its normalized title.
        /// </summary>
        /// <param name="normalizedTitle">The normalized title of the movie.</param>
        /// <returns>The ID of the movie, or null if the movie does not exist.</returns>
        public async Task<int?> GetMovieId(string normalizedTitle)
        {
            var movieId = await _unitOfWork.Repository<Movie>().Queryable()
                .Where(a => a.NormalizedTitle == normalizedTitle)
                .Select(a => a.MovieId)
                .FirstOrDefaultAsync();
            if (movieId == 0)
                return null;
            return movieId;
        }
    }
}
