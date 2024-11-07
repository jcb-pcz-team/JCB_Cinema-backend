using AutoMapper;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JCB_Cinema.Application.Servicies
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IUserContextService _userContextService;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly IMapper _mapper;

        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _userContextService = userContextService;
        }

        public void CreateFillEntity<T>(T entity)
           where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }
            entity.IsDeleted = false;
            entity.Created = DateTime.UtcNow;
            entity.Creator = userName;
        }

        public void UpdateFillEntity<T>(T entity)
            where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }
            entity.IsDeleted = false;
            entity.Modified = DateTime.UtcNow;
            entity.Modifier = userName;
        }
        public void Delete<T>(T entity)
            where T : EntityBase
        {
            var userName = _userContextService.GetUserName();
            if (userName == null)
            {
                throw new UnauthorizedAccessException();
            }
            entity.IsDeleted = true;
            entity.Modified = DateTime.UtcNow;
            entity.Modifier = userName;
        }
    }
}
