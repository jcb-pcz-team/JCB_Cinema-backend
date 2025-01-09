using AutoMapper;
using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Interfaces;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;
using JCB_Cinema.Domain.Entities;
using JCB_Cinema.Domain.Interface;
using JCB_Cinema.Infrastructure.Data.Interfaces;
using JCB_Cinema.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JCB_Cinema.Application.Servicies
{
    public class PhotoService : ServiceBase, IPhotoService
    {
        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IUserContextService userContextService) : base(unitOfWork, mapper, userManager, userContextService) { }

        public async Task Delete(int id)
        {
            await _unitOfWork.Repository<Photo>().DeleteAsync(id);
        }

        public async Task<PhotoDTO?> Get(string description)
        {
            var normalizedDesc = description.NormalizeString();
            var entity = await _unitOfWork.Repository<Photo>().Queryable()
                .FirstOrDefaultAsync(a => a.Description == normalizedDesc);
            return _mapper.Map<PhotoDTO?>(entity);
        }

        public Task<IList<PhotoDTO?>> Get(QueryPhotos query)
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoDTO> Update(UpdatePhoto photo)
        {
            if (photo.File == null || photo.File.Length == 0)
            {
                throw new NullReferenceException();
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await photo.File.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var newPhoto = new Photo
            {
                Id = photo.Id,
                Bytes = fileBytes,
                Description = photo.Description == null ? null : photo.Description.NormalizeString(),
                FileExtension = Path.GetExtension(photo.File.FileName),
                Size = photo.File.Length / 1024.0 // Rozmiar w KB
            };

            await _unitOfWork.Repository<Photo>().UpdateAsync(newPhoto);

            return _mapper.Map<PhotoDTO>(newPhoto);
        }

        public async Task<PhotoDTO?> UploadPhoto(UploadPhoto photo)
        {
            if (photo.File == null || photo.File.Length == 0)
            {
                throw new NullReferenceException();
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await photo.File.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var newPhoto = new Photo
            {
                Bytes = fileBytes,
                Description = photo.Description == null ? null : photo.Description.NormalizeString(),
                FileExtension = Path.GetExtension(photo.File.FileName),
                Size = photo.File.Length / 1024.0 // Rozmiar w KB
            };

            await _unitOfWork.Repository<Photo>().AddAsync(newPhoto);

            return _mapper.Map<PhotoDTO>(newPhoto);
        }
    }
}
