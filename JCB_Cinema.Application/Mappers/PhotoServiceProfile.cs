using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    public class PhotoServiceProfile : Profile
    {
        public PhotoServiceProfile()
        {
            CreateMap<Photo, PhotoDTO>().ReverseMap();
        }
    }
}