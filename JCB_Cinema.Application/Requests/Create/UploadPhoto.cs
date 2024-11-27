using Microsoft.AspNetCore.Http;

namespace JCB_Cinema.Application.Requests.Create
{
    public class UploadPhoto
    {
        public IFormFile File { get; set; } = null!;
        public string? Description { get; set; }
    }
}
