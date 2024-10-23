using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.ValueObjects;
using JCB_Cinema.Tools;

namespace JCB_Cinema.Application.Mappers
{
    public class ScheduleServiceProfile : Profile
    {
        public ScheduleServiceProfile()
        {
            CreateMap<Schedule, GetScheduleDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Screenings.Any() ? src.Screenings.First().Movie.Title : string.Empty))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Screenings.Any() && src.Screenings.First().Movie.Genre != null
                                                                            ? src.Screenings.First().Movie.Genre!.GetDescription()
                                                                            : ScreenType.TwoD.GetDescription()))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Screenings.Any() ? src.Screenings.First().Movie.Duration : 0))
                .ForMember(dest => dest.ScreenType, opt => opt.MapFrom(src => src.Screenings.Any() ? src.Screenings.First().ScreenType.GetDescription() : string.Empty))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings));

            CreateMap<GetScheduleDTO, Schedule>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings))
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore());
        }
    }
}