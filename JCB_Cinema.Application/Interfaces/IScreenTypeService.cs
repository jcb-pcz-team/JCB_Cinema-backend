using JCB_Cinema.Application.DTOs;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IScreenTypeService
    {
        public Task<IList<GetScreenTypeDTO>> Get();
    }
}
