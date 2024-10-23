using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Infrastructure.Data.Interfaces;
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

        public async Task<IList<GetMovieProjectionDTO>?> Get(RequestMovieProjection query)
        {
            var entitiesQuery = _unitOfWork.Repository<MovieProjection>().Queryable();
            if (!string.IsNullOrWhiteSpace(query.ScreenTypeName))
            {
                entitiesQuery.Where(a => a.ScreeningTime.Equals(query.ScreenTypeName));
            }

            var entitiesList = await entitiesQuery.ToListAsync();
            if (entitiesList == null)
            {
                return null;
            }

            var mapped = _mapper.Map<IList<GetMovieProjectionDTO>>(entitiesList);
            return mapped;
        }
    }
}
