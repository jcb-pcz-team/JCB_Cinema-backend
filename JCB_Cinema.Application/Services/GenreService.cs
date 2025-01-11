using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class for managing movie genres. Provides functionality to retrieve a list of movie genres.
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance for mapping genre values to DTOs.</param>
        public GenreService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all movie genres.
        /// </summary>
        /// <returns>A list of <see cref="GetGenreDTO"/> representing all available genres.</returns>
        public async Task<IList<GetGenreDTO>> Get()
        {
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var genredDTO = _mapper.Map<IList<GetGenreDTO>>(genres);
            return await Task.FromResult(genredDTO);
        }
    }
}
