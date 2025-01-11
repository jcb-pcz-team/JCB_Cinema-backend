namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a photo entity, storing image data and related metadata.
    /// </summary>
    public class Photo : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the photo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the byte array representing the photo content.
        /// </summary>
        public byte[]? Bytes { get; set; }

        /// <summary>
        /// Gets or sets a description of the photo.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the photo (e.g., ".jpg", ".png").
        /// </summary>
        public string FileExtension { get; set; } = null!;

        /// <summary>
        /// Gets or sets the size of the photo in kilobytes (KB).
        /// </summary>
        public double? Size { get; set; }

        /// <summary>
        /// Gets the unique key for the photo entity.
        /// </summary>
        public override object Key => Id;
    }
}
