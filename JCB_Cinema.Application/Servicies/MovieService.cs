using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces.Servicies;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IList<GetMovieDTO>> Get([FromQuery] RequestMovies query)
        {
            var entities = await _unitOfWork.Repository<Movie>().Queryable().ToListAsync();
            return _mapper.Map<IList<GetMovieDTO>>(entities);
        }
    }
}
