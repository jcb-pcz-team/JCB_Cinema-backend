using Microsoft.AspNetCore.Http;

namespace JCB_Cinema.Application.Requests.Create
{
    /// <summary>
    /// Represents a request to update a photo in the system.
    /// </summary>
    public class UpdatePhoto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the photo to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new file associated with the photo. 
        /// The file should be uploaded as part of the request.
        /// </summary>
        public IFormFile File { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the photo.
        /// This field is optional and can be used to provide additional information about the photo.
        /// </summary>
        public string? Description { get; set; }
    }
}

