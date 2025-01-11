using Microsoft.AspNetCore.Http;

namespace JCB_Cinema.Application.Requests.Create
{
    /// <summary>
    /// Represents a request to upload a photo.
    /// </summary>
    public class UploadPhoto
    {
        /// <summary>
        /// Gets or sets the file to be uploaded.
        /// This field is required.
        /// </summary>
        public IFormFile File { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the photo.
        /// This field is optional.
        /// </summary>
        public string? Description { get; set; }
    }
}
