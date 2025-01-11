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
using JCB_Cinema.Application.Servicies;

namespace JCB_Cinema.Application.Services
{
    /// <summary>
    /// Service for managing photo operations in the cinema application.
    /// </summary>
    /// <remarks>
    /// Provides methods for uploading, updating, retrieving, and deleting photos associated with movies or other entities.
    /// </remarks>
    public class PhotoService : ServiceBase, IPhotoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for database operations.</param>
        /// <param name="mapper">Mapper for object transformations.</param>
        /// <param name="userManager">Manager for user authentication and roles.</param>
        /// <param name="userContextService">Service to retrieve the current user's context.</param>
        public PhotoService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IUserContextService userContextService
        ) : base(unitOfWork, mapper, userManager, userContextService) { }

        /// <summary>
        /// Deletes a photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Delete(int id)
        {
            await _unitOfWork.Repository<Photo>().DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves a photo by its description.
        /// </summary>
        /// <param name="description">The description of the photo.</param>
        /// <returns>A DTO containing the photo details, or null if not found.</returns>
        public async Task<PhotoDTO?> Get(string description)
        {
            var normalizedDesc = description.NormalizeString();
            var entity = await _unitOfWork.Repository<Photo>().Queryable()
                .FirstOrDefaultAsync(a => a.Description == normalizedDesc);
            return _mapper.Map<PhotoDTO?>(entity);
        }

        /// <summary>
        /// Retrieves a list of photos based on the specified query.
        /// </summary>
        /// <param name="query">The query parameters for filtering photos.</param>
        /// <returns>A task representing the asynchronous operation, returning a list of photo DTOs.</returns>
        /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
        public Task<IList<PhotoDTO?>> Get(QueryPhotos query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing photo with new data.
        /// </summary>
        /// <param name="photo">The update request containing new photo data.</param>
        /// <returns>A DTO containing the updated photo details.</returns>
        /// <exception cref="NullReferenceException">Thrown if the provided file is null or empty.</exception>
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
                Size = photo.File.Length / 1024.0 // Size in KB
            };

            await _unitOfWork.Repository<Photo>().UpdateAsync(newPhoto);

            return _mapper.Map<PhotoDTO>(newPhoto);
        }

        /// <summary>
        /// Uploads a new photo to the database.
        /// </summary>
        /// <param name="photo">The upload request containing photo data.</param>
        /// <returns>A DTO containing the details of the uploaded photo.</returns>
        /// <exception cref="NullReferenceException">Thrown if the provided file is null or empty.</exception>
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
                Size = photo.File.Length / 1024.0 // Size in KB
            };

            await _unitOfWork.Repository<Photo>().AddAsync(newPhoto);

            return _mapper.Map<PhotoDTO>(newPhoto);
        }
    }
}
