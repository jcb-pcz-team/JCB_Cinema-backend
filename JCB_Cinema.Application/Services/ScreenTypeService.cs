using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing screen types in the cinema application.
    /// </summary>
    /// <remarks>
    /// This service provides methods to retrieve information about available screen types.
    /// </remarks>
    public class ScreenTypeService : IScreenTypeService
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenTypeService"/> class.
        /// </summary>
        /// <param name="mapper">An instance of <see cref="IMapper"/> for mapping objects.</param>
        public ScreenTypeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all available screen types.
        /// </summary>
        /// <remarks>
        /// The method uses the <see cref="ScreenType"/> enum to get all possible screen types, maps them to <see cref="GetScreenTypeDTO"/>, and returns the result.
        /// </remarks>
        /// <returns>
        /// A task representing the asynchronous operation, returning a list of <see cref="GetScreenTypeDTO"/> objects.
        /// </returns>
        public async Task<IList<GetScreenTypeDTO>> Get()
        {
            // Retrieve all values from the ScreenType enum.
            var screenTypes = Enum.GetValues(typeof(ScreenType)).Cast<ScreenType>().ToList();

            // Map the ScreenType enum values to DTO objects.
            var screenTypeDTOs = _mapper.Map<IList<GetScreenTypeDTO>>(screenTypes);

            // Return the mapped DTOs as a completed task.
            return await Task.FromResult(screenTypeDTOs);
        }
    }
}
