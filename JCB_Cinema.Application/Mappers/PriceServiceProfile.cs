using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="Price"/> value object
    /// and its related data transfer object (DTO) <see cref="GetPriceDTO"/>.
    /// </summary>
    public class PriceServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="Price"/> to <see cref="GetPriceDTO"/> 
        /// and the reverse mapping from <see cref="GetPriceDTO"/> to <see cref="Price"/>.
        /// </summary>
        public PriceServiceProfile()
        {
            // Mapping from Price to GetPriceDTO
            // AmountInCents is mapped to Ammount and Currency is converted to uppercase
            CreateMap<Price, GetPriceDTO>()
                .ForMember(dest => dest.Ammount, opt => opt.MapFrom(src => src.AmountInCents))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToUpper()))
                .ReverseMap(); // Enables reverse mapping from GetPriceDTO to Price
        }
    }
}
