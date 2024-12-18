﻿using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoDTO?> UploadPhoto(UploadPhoto photo);
        Task<PhotoDTO?> Get(string description);
        Task<IList<PhotoDTO?>> Get(QueryPhotos query);
        Task Delete(int id);
        Task<PhotoDTO> Update(UpdatePhoto photo);
    }
}
