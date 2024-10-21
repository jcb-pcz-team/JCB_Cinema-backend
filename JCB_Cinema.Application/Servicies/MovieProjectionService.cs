using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces.Servicies;
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

        public async Task<IEnumerable<GetMovieProjectionDTO>> Get(string screenType)
        {
            Enum.TryParse<ScreenType>(screenType, out var parsedScreenType);
            var allMovieProjections = _unitOfWork.Repository<MovieProjection>().Queryable().Where(x => x.ScreenType == parsedScreenType);
            var allMovieProjectionsList = await allMovieProjections.ToListAsync();
            var mapped = _mapper.Map<IList<GetMovieProjectionDTO>>(allMovieProjectionsList);
            return mapped;
        }
    }
}
