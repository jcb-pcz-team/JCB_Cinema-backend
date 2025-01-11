namespace JCB_Cinema.Application.DTOs.AdminPanel
{
    /// <summary>
    /// Data Transfer Object representing a cinema hall in the admin panel.
    /// </summary>
    public class AdmCinemaHallDTO
    {
        /// <summary>
        /// Gets or sets the name of the cinema hall.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the name of the cinema hall.
        /// </value>
        public string CinemaHallName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the total number of seats in the cinema hall.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the total number of seats.
        /// </value>
        public int TotalSeats { get; set; }
    }
}
