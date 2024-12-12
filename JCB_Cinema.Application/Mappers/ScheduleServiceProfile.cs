using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class ScheduleServiceProfile : Profile
    {
        public ScheduleServiceProfile()
        {
            CreateMap<Schedule, GetScheduleDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings));

            CreateMap<GetScheduleDTO, Schedule>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings))
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore());
        }
    }
}