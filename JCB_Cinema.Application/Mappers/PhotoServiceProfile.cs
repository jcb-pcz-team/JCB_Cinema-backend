using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class PhotoServiceProfile : Profile
    {
        public PhotoServiceProfile()
        {
            CreateMap<Photo, GetPosterDTO>()
                .ForMember(dest => dest.PosterId, opt => opt.MapFrom(src => src.Id));

            CreateMap<GetPosterDTO, Photo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PosterId))
                .ForMember(dest => dest.Bytes, opt => opt.Ignore()) // Ignore Bytes in mapping since it's not part of the DTO
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.FileExtension, opt => opt.Ignore())
                .ForMember(dest => dest.Size, opt => opt.Ignore());
        }
    }
}