using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Domain.Entities;

namespace JCB_Cinema.Application.Mappers
{
    /// <summary>
    /// AutoMapper profile class for configuring mappings between the <see cref="Photo"/> entity
    /// and its related data transfer objects (DTOs), including <see cref="PhotoDTO"/> and <see cref="UpdatePhoto"/>.
    /// </summary>
    public class PhotoServiceProfile : Profile
    {
        /// <summary>
        /// Configures the object mappings for <see cref="Photo"/> to <see cref="PhotoDTO"/> 
        /// and the mapping for <see cref="UpdatePhoto"/> to <see cref="Photo"/>.
        /// </summary>
        public PhotoServiceProfile()
        {
            // Mapping from Photo to PhotoDTO and vice versa (ReverseMap handles the reverse mapping automatically)
            CreateMap<Photo, PhotoDTO>().ReverseMap();

            // Mapping from UpdatePhoto to Photo
            CreateMap<UpdatePhoto, Photo>();
        }
    }
}
