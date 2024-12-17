using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Servicies
{
    public class ScreenTypeService  : IScreenTypeService
    {
        private readonly IMapper _mapper;

        public ScreenTypeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IList<GetScreenTypeDTO>> Get()
        {
            var ScreenTypes = Enum.GetValues(typeof(ScreenType)).Cast<ScreenType>().ToList();
            var ScreenTypedDTO = _mapper.Map<IList<GetScreenTypeDTO>>(ScreenTypes);
            return await Task.FromResult(ScreenTypedDTO);
        }
    }
}
