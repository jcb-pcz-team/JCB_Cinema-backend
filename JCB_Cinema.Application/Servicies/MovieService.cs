using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetMovieDTO>?> Get(int genreId)
        {
            var allMovies = await _unitOfWork.Repository<Movie>().Queryable().Where(m => m.Genre.HasValue && (int)m.Genre.Value == genreId).ToListAsync();
            return allMovies == null ? null : _mapper.Map<IList<GetMovieDTO>>(allMovies);
        }

        public async Task<IList<GetMovieDTO>?> Get(string genreName)
        {
            var allMovies = _unitOfWork.Repository<Movie>().Queryable();
            if (!string.IsNullOrWhiteSpace(genreName))
            {
                Genre? genreValue = EnumExtensions.GetValueFromDescription<Genre>(genreName);
                allMovies = allMovies.Where(m => m.Genre == genreValue);
            }
            var moviesList = await allMovies.ToListAsync();
            return moviesList == null ? null : _mapper.Map<IList<GetMovieDTO>>(moviesList);
        }
    }
}
