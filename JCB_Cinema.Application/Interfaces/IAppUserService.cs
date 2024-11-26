﻿using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IAppUserService
    {
        public Task<GetAppUserDTO?> GetAppUserAsync(RequestAppUser request);
        public Task PutAppUserAsync(PutAppUserDetails appUserDTO);
    }
}