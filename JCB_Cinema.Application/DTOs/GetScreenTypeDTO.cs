namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a screen type used in a cinema.
    /// </summary>
    public class GetScreenTypeDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the screen type.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the unique ID of the screen type.
        /// </value>
        public int ScreenTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the screen type (e.g., IMAX, Standard).
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the name of the screen type.
        /// This property is initialized as an empty string by default.
        /// </value>
        public string ScreenTypeName { get; set; } = string.Empty;
    }
}
