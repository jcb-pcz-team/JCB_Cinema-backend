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
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Servicies
{
    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
        }

        public async Task<IList<GetMovieDTO>?> Get(RequestMovies request)
        {
            var query = _unitOfWork.Repository<Movie>().Queryable();
            if (request.GenreId.HasValue)
            {
                query = query.Where(m => (int?)m.Genre == request.GenreId);
            }
            else if (!string.IsNullOrWhiteSpace(request.GenreName))
            {
                Genre? genreValue = EnumExtensions.GetValueFromDescription<Genre>(request.GenreName);
                query = query.Where(m => m.Genre == genreValue);
            }

            var moviesList = await query.ToListAsync();
            return moviesList == null ? null : _mapper.Map<IList<GetMovieDTO>>(moviesList);
        }

        public async Task<GetMovieDTO?> GetDetails(int id)
        {
            var query = await _unitOfWork.Repository<Movie>().Queryable().FirstOrDefaultAsync(m => m.MovieId == id);
            return query == null ? null : _mapper.Map<GetMovieDTO>(query);
        }

        public async Task<bool> IsAny(Expression<Func<Movie, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<Movie>().Queryable().AnyAsync(predicate);
            return entity;
        }
    }
}
