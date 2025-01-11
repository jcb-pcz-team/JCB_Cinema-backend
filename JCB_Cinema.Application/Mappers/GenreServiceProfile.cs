using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="Genre"/> entity 
    /// and the <see cref="GetGenreDTO"/> data transfer object (DTO).
    /// </summary>
    public class GenreServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="Genre"/> to <see cref="GetGenreDTO"/> 
        /// and vice versa. It also includes the mapping of the genre description.
        /// </summary>
        public GenreServiceProfile()
        {
            // Mapping from Genre to GetGenreDTO
            CreateMap<Genre, GetGenreDTO>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => (int?)src)) // Mapping Genre to GenreId
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.GetDescription())) // Mapping Genre description to GenreName
                .ReverseMap(); // Reverse the mapping for GetGenreDTO to Genre
        }
    }
}
