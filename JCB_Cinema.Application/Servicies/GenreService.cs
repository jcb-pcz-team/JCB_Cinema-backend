using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces.Servicies;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Infrastructure.Data.Interfaces;

namespace JCB_Cinema.Application.Servicies
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetGenreDTO>> Get()
        {
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var genredDTO = _mapper.Map<IList<GetGenreDTO>>(genres);
            return await Task.FromResult(genredDTO);
        }
    }
}
