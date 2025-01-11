using JCB_Cinema.Application.DTOs;
using JCB_Cinema.Application.Requests.Create;
using JCB_Cinema.Application.Requests.Queries;

namespace JCB_Cinema.Application.Interfaces
{
    /// <summary>
    /// Interface for the service responsible for managing photo operations.
    /// </summary>
    public interface IPhotoService
    {

#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously uploads a photo.
        /// </summary>
        /// <param name="photo">
        /// An <see cref="UploadPhoto"/> containing the details of the photo to upload.
        /// </param>
        /// <returns>
        /// A <see cref="Task{PhotoDTO?}"/> representing the asynchronous operation. The result contains a <see cref="PhotoDTO"/>
        /// with the uploaded photo details, or null if the upload fails.
        /// </returns>
        Task<PhotoDTO?> UploadPhoto(UploadPhoto photo);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a photo by its description.
        /// </summary>
        /// <param name="description">
        /// A <see cref="string"/> representing the description of the photo to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Task{PhotoDTO?}"/> representing the asynchronous operation. The result contains a <see cref="PhotoDTO"/>
        /// with the photo details, or null if no photo is found with the specified description.
        /// </returns>
        Task<PhotoDTO?> Get(string description);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Asynchronously retrieves a list of photos based on the provided query parameters.
        /// </summary>
        /// <param name="query">
        /// A <see cref="QueryPhotos"/> containing the search criteria for retrieving photos.
        /// </param>
        /// <returns>
        /// A <see cref="Task{IList{PhotoDTO?}}"/> representing the asynchronous operation. The result contains a list of 
        /// <see cref="PhotoDTO"/> objects or null if no photos match the query.
        /// </returns>
        Task<IList<PhotoDTO?>> Get(QueryPhotos query);
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

        /// <summary>
        /// Asynchronously deletes a photo by its ID.
        /// </summary>
        /// <param name="id">
        /// An <see cref="int"/> representing the ID of the photo to delete.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task Delete(int id);

        /// <summary>
        /// Asynchronously updates the details of an existing photo.
        /// </summary>
        /// <param name="photo">
        /// An <see cref="UpdatePhoto"/> containing the updated details of the photo.
        /// </param>
        /// <returns>
        /// A <see cref="Task{PhotoDTO}"/> representing the asynchronous operation. The result contains a <see cref="PhotoDTO"/>
        /// with the updated photo details.
        /// </returns>
        Task<PhotoDTO> Update(UpdatePhoto photo);
    }
}
