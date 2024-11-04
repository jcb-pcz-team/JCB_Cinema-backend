using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CinemaHallService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetMovieProjectionDTO>?> Get(int cinemaHallId)
        {
            var movieProjections = _unitOfWork.Repository<MovieProjection>().Queryable();
            movieProjections = movieProjections.Where(x => x.CinemaHallId == cinemaHallId);
            var movieProjectionsList = await movieProjections.ToListAsync();
            if (movieProjectionsList == null)
                return null;
            var entities = _mapper.Map<IList<GetMovieProjectionDTO>>(movieProjectionsList);
            return entities;
        }
    }
}
