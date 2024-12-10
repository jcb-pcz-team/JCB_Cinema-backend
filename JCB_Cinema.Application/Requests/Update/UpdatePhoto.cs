using Microsoft.AspNetCore.Http;

namespace JCB_Cinema.Application.Requests.Create
{
    public class UpdatePhoto
    {
        public int Id {  get; set; }
        public IFormFile File { get; set; } = null!;
        public string? Description { get; set; }
    }
}
