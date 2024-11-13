﻿using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;
using JCB_Cinema.Domain.Entities;
using System.Linq.Expressions;

namespace JCB_Cinema.Application.Interfaces
{
    public interface ICinemaHallService
    {
        Task<bool> IsAny(Expression<Func<CinemaHall, bool>> predicate);
        public Task<IList<GetCinemaHallDTO>?> Get(RequestCinemaHall request);
        Task<GetCinemaHallDTO?> Get(int id);
    }
}