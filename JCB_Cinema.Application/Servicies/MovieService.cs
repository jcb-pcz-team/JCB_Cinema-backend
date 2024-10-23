using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
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

        public async Task<IList<GetMovieDTO>> Get(RequestMovies query)
        {
            var allMovies = _unitOfWork.Repository<Movie>().Queryable();

            // Filter by Genre Id
            if (query.GenreId.HasValue)
            {
                allMovies = allMovies.Where(m => m.Genre.HasValue && (int)m.Genre.Value == query.GenreId.Value);
            }
            // Filter by Genre name
            if (!string.IsNullOrWhiteSpace(query.GenreName))
            {
                allMovies = allMovies.Where(m => m.Genre.HasValue.ToString().Equals(query.GenreName, StringComparison.OrdinalIgnoreCase));
            }
            //Filter by Release Date
            if (query.Release.HasValue)
            {
                allMovies = allMovies
                    .Where(m => m.ReleaseDate.HasValue
                    && DateOnly.FromDateTime(m.ReleaseDate.Value.Date) == query.Release.Value);
            }


            var moviesList = await allMovies.ToListAsync();
            var mappedList = _mapper.Map<IList<GetMovieDTO>>(moviesList);
            return mappedList;
        }
    }
}
