namespace JCB_Cinema.Application.DTOs
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string FileExtension { get; set; } = null!;
        public double? Size { get; set; }
        public byte[]? Bytes { get; set; }
    }
}
