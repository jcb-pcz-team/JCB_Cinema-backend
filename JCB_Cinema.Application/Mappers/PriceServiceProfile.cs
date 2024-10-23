using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Mappers
{
    public class PriceServiceProfile : Profile
    {
        public PriceServiceProfile()
        {
            CreateMap<Price, GetPriceDTO>()
                .ForMember(dest => dest.Ammount, opt => opt.MapFrom(src => src.AmountInCents))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToUpper()))
                .ReverseMap();
        }
    }
}