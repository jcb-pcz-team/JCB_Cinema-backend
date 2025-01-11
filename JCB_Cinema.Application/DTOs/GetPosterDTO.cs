namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a movie poster.
    /// </summary>
    public class GetPosterDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the movie poster.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the unique ID of the movie poster.
        /// </value>
        public int PosterId { get; set; }
    }
}
