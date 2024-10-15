namespace JCB_Cinema.Domain.Entities
{
    public class Photo : EntityBase
    {
        //https://learn.microsoft.com/en-us/answers/questions/682240/best-practice-for-saving-image-in-database
        public int Id { get; set; }
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
        public string FileExtension { get; set; } = null!;
        public double? Size { get; set; }

        public override int Key => Id;
    }
}
