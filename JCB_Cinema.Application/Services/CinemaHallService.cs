using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Services;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Servicies
{
    /// <summary>
    /// Service class that provides operations related to cinema halls, including fetching cinema hall details, 
    /// checking if a cinema hall exists based on a predicate, and retrieving specific cinema hall information.
    /// </summary>
    public class CinemaHallService : ServiceBase, ICinemaHallService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CinemaHallService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for mapping data objects.</param>
        /// <param name="userManager">The user manager for managing user-related operations.</param>
        /// <param name="userContextService">The service that provides the current user's context.</param>
        public CinemaHallService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService)
            : base(unitOfWork, mapper, userManager, userContextService) { }

        /// <summary>
        /// Retrieves a list of cinema halls based on the specified query parameters.
        /// Optionally filters by cinema hall name.
        /// </summary>
        /// <param name="request">The request containing query parameters for filtering cinema halls.</param>
        /// <returns>A list of <see cref="GetCinemaHallDTO"/> representing the cinema halls, or null if no results are found.</returns>
        public async Task<IList<GetCinemaHallDTO>?> Get(QueryCinemaHall request)
        {
            var query = _unitOfWork.Repository<CinemaHall>().Queryable();
            query = query.Include(a => a.Seats);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(a => a.Name.Equals(request.Name));
            }

            var entities = await query.ToListAsync();
            return entities == null ? null : _mapper.Map<IList<GetCinemaHallDTO>>(entities);
        }

        /// <summary>
        /// Retrieves a specific cinema hall by its ID.
        /// </summary>
        /// <param name="id">The ID of the cinema hall to retrieve.</param>
        /// <returns>A <see cref="GetCinemaHallDTO"/> representing the cinema hall, or null if not found.</returns>
        public async Task<GetCinemaHallDTO?> Get(int id)
        {
            var entity = await _unitOfWork.Repository<CinemaHall>()
                .Queryable()
                .Include(a => a.Seats)
                .FirstOrDefaultAsync(a => a.CinemaHallId == id);
            return entity == null ? null : _mapper.Map<GetCinemaHallDTO>(entity);
        }

        /// <summary>
        /// Checks if any cinema hall exists that matches the provided predicate.
        /// </summary>
        /// <param name="predicate">The expression to match against cinema halls.</param>
        /// <returns><c>true</c> if at least one cinema hall matches the predicate; otherwise, <c>false</c>.</returns>
        public async Task<bool> IsAny(Expression<Func<CinemaHall, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<CinemaHall>()
                .Queryable()
                .Include(a => a.Seats)
                .AnyAsync(predicate);
            return entity;
        }
    }
}
