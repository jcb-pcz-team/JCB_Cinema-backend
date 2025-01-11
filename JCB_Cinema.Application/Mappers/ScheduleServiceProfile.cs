using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="Schedule"/> entity
    /// and its related data transfer object (DTO) <see cref="GetScheduleDTO"/>.
    /// </summary>
    public class ScheduleServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="Schedule"/> to <see cref="GetScheduleDTO"/> 
        /// and the reverse mapping from <see cref="GetScheduleDTO"/> to <see cref="Schedule"/>.
        /// </summary>
        public ScheduleServiceProfile()
        {
            // Mapping from Schedule to GetScheduleDTO
            // Date and Screenings properties are directly mapped
            CreateMap<Schedule, GetScheduleDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings));

            // Mapping from GetScheduleDTO to Schedule
            // Date and Screenings properties are directly mapped, ScheduleId is ignored
            CreateMap<GetScheduleDTO, Schedule>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings))
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore()); // ScheduleId is ignored as it is likely auto-generated
        }
    }
}
