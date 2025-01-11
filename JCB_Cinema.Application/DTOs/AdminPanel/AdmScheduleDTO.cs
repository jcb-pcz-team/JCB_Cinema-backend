namespace JCB_Cinema.Application.DTOs.AdminPanel
{
    /// <summary>
    /// Data Transfer Object representing a schedule overview in the admin panel.
    /// </summary>
    public class AdmScheduleDTO
    {
        /// <summary>
        /// Gets or sets the date for the schedule.
        /// </summary>
        /// <value>
        /// A <see cref="DateOnly"/> representing the schedule date.
        /// </value>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the total number of movies scheduled for the date.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the number of movies.
        /// </value>
        public int MovieCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of movie projections scheduled for the date.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the number of movie projections.
        /// </value>
        public int MovieProjectionsCount { get; set; }

        /// <summary>
        /// Gets or sets the number of active cinema halls for the schedule.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the number of active cinema halls.
        /// </value>
        public int ActiveCinemaHalls { get; set; }

        /// <summary>
        /// Gets or sets the total number of tickets booked for the date.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the total number of booking tickets.
        /// </value>
        public int TotalBookingTickets { get; set; }
    }
}
