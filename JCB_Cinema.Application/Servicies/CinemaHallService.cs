﻿using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Servicies
{
    public class CinemaHallService : ServiceBase, ICinemaHallService
    {
        public CinemaHallService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService)
        {
        }

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

        public async Task<GetCinemaHallDTO?> Get(int id)
        {
            var entity = await _unitOfWork.Repository<CinemaHall>()
                .Queryable()
                .Include(a => a.Seats)
                .FirstOrDefaultAsync(a => a.CinemaHallId == id);
            return entity == null ? null : _mapper.Map<GetCinemaHallDTO>(entity);
        }

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
