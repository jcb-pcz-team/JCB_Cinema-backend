namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the details of a cinema hall.
    /// </summary>
    public class GetCinemaHallDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cinema hall.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the cinema hall's unique ID.
        /// </value>
        public int CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the name of the cinema hall.
        /// </summary>
        /// <value>
        /// A nullable <see cref="string"/> representing the name of the cinema hall.
        /// </value>
        public string? Name { get; set; }
    }
}
