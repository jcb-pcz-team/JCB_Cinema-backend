namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a photo.
    /// </summary>
    public class PhotoDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the photo.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the unique ID of the photo.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the photo.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the description or caption of the photo.
        /// This property can be null if no description is provided.
        /// </value>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the photo (e.g., "jpg", "png").
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the file extension of the photo.
        /// This property is initialized with a non-nullable value.
        /// </value>
        public string FileExtension { get; set; } = null!;

        /// <summary>
        /// Gets or sets the size of the photo in bytes.
        /// </summary>
        /// <value>
        /// A nullable <see cref="double"/> representing the size of the photo in bytes.
        /// This property can be null if the size is not specified.
        /// </value>
        public double? Size { get; set; }


#pragma warning disable CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref

#pragma warning disable CS1658 // Ostrzeżenie przesłania błąd
        /// <summary>
        /// Gets or sets the byte array representing the contents of the photo.
        /// </summary>
        /// <value>
        /// A nullable <see cref="byte[]"/> array containing the binary data of the photo.
        /// This property can be null if the bytes are not available.
        /// </value>
        public byte[]? Bytes { get; set; }
#pragma warning restore CS1658 // Ostrzeżenie przesłania błąd
#pragma warning restore CS1584 // Komentarz XML zawiera składniowo niepoprawny atrybut cref
    }
}
