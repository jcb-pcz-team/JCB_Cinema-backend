namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the schedule of movie screenings on a specific date.
    /// </summary>
    public class GetScheduleDTO
    {
        /// <summary>
        /// Gets or sets the date for the schedule.
        /// </summary>
        /// <value>
        /// A <see cref="DateOnly"/> representing the date of the movie screenings.
        /// </value>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the list of movie projections (screenings) for the specified date.
        /// </summary>
        /// <value>
        /// A <see cref="IList{GetMovieProjectionDTO}"/> containing the movie projections (screenings) for the given date.
        /// It is initialized with an empty list.
        /// </value>
        public IList<GetMovieProjectionDTO> Screenings { get; set; } = new List<GetMovieProjectionDTO>();
    }
}
