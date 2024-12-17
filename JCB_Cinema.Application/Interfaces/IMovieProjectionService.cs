﻿using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Application.Requests.Update;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IMovieProjectionService
    {
        public Task<IList<GetMovieProjectionDTO>?> Get(QueryMovieProjections request);
        public Task<GetMovieProjectionDTO?> GetDetails(int id);
        public Task UpdateMovieProjection(int projectionId, UpdateMovieProjectionRequest movieProjectionDTO);
        public Task AddMovieProjection(AddMovieProjectionRequest movieProjectionDTO);
        public Task DeleteMovieProjection(int projectionId);
    }
}
