using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
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

        public async Task<IList<GetMovieProjectionDTO>?> Get(RequestMovieProjection request)
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
            var entities = await query.ToListAsync();
            return entities == null ? null : _mapper.Map<IList<GetMovieProjectionDTO>>(entities);
        }
    }
}
