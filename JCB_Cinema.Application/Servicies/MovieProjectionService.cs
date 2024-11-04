using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class MovieProjectionService : IMovieProjectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieProjectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetMovieProjectionDTO>?> Get([FromQuery] string screenType)
        {
            var entitiesQuery = _unitOfWork.Repository<MovieProjection>().Queryable();
            if (!string.IsNullOrWhiteSpace(screenType))
                entitiesQuery.Where(a => a.ScreeningTime.Equals(screenType));
            var entitiesList = await entitiesQuery.ToListAsync();
            return entitiesList == null ? null : _mapper.Map<IList<GetMovieProjectionDTO>>(entitiesList);
        }
    }
}
