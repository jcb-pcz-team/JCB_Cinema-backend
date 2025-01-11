using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="ScreenType"/> enum
    /// and its related data transfer object (DTO) <see cref="GetScreenTypeDTO"/>.
    /// </summary>
    public class ScreenTypeServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="ScreenType"/> to <see cref="GetScreenTypeDTO"/> 
        /// and the reverse mapping from <see cref="GetScreenTypeDTO"/> to <see cref="ScreenType"/>.
        /// </summary>
        public ScreenTypeServiceProfile()
        {
            // Mapping from ScreenType to GetScreenTypeDTO
            // ScreenType is mapped to its integer value, and its description is mapped to ScreenTypeName
            CreateMap<ScreenType, GetScreenTypeDTO>()
               .ForMember(dest => dest.ScreenTypeId, opt => opt.MapFrom(src => (int?)src)) // Maps enum to its integer value
               .ForMember(dest => dest.ScreenTypeName, opt => opt.MapFrom(src => src.GetDescription())) // Maps enum description to ScreenTypeName
               .ReverseMap(); // Reverse map to support mapping from GetScreenTypeDTO to ScreenType
        }
    }
}
