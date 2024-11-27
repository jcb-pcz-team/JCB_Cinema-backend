using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoDTO?> UploadPhoto(UploadPhoto photo);
        Task<PhotoDTO?> Get(int id);
        Task<IList<PhotoDTO?>> Get(QueryPhotos query);
    }
}
