using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Servicies
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper)
        {
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
