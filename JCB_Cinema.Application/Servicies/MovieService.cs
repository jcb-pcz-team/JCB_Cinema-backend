﻿using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Infrastructure.Data.Seed;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Servicies
{
    public class MovieService : ServiceBase, IMovieService
    {

        private readonly IPhotoService _photoService;
        public MovieService(IPhotoService photoService, IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
            _photoService = photoService;
        }

        public async Task AddMovie(AddMovieDTO addMovie)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
                throw new UnauthorizedAccessException();

            addMovie.Title = addMovie.Title.NormalizeMovieName();
            Movie movie = _mapper.Map<Movie>(addMovie);

            Photo? photo = null;
            if (addMovie.Poster != null)
            {
                addMovie.Poster.Description = addMovie.Title;
                var photoDTO = await _photoService.UploadPhoto(addMovie.Poster);
                photo = _mapper.Map<Photo>(photoDTO);
                movie.PhotoId = photo.Id;
            }

            await _unitOfWork.Repository<Movie>().AddAsync(movie);
        }

        public async Task<IList<GetMovieDTO>?> Get(QueryMovies request)
        {
            var query = _unitOfWork.Repository<Movie>().Queryable();
            query = query.Include(a => a.Poster);

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

        public async Task<GetMovieDTO?> GetDetails(string title)
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Poster)
                .FirstOrDefaultAsync(m => m.Title == title.NormalizeMovieName());
            return query == null ? null : _mapper.Map<GetMovieDTO>(query);
        }

        public async Task<IList<GetMovieDTO>?> GetUpcoming()
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Poster)
                .Where(m => m.ReleaseDate > DateOnly.FromDateTime(DateTime.UtcNow))
                .ToListAsync();
            return query == null ? null : _mapper.Map<IList<GetMovieDTO>>(query);
        }

        public async Task<bool> IsAny(Expression<Func<Movie, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<Movie>().Queryable()
                .Include(a => a.Poster)
                .AnyAsync(predicate);
            return entity;
        }

        public async Task UpdateMovie(UpdateMovieDTO updatemovie)
        {
            var currentUserName = _userContextService.GetUserName();

            if (string.IsNullOrEmpty(currentUserName))
                throw new UnauthorizedAccessException();

            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser == null)
                throw new UnauthorizedAccessException();

            if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                var movie = _mapper.Map<Movie>(updatemovie);
                await _unitOfWork.Repository<Movie>().UpdateAsync(movie);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
