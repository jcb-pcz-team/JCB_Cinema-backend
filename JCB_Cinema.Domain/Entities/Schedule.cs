namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a schedule entity that contains a collection of movie projections for a specific date.
    /// </summary>
    public class Schedule : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the schedule.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// Gets or sets the date for the schedule.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the list of movie projections (screenings) scheduled for the specified date.
        /// </summary>
        public List<MovieProjection> Screenings { get; set; } = new List<MovieProjection>();

        /// <summary>
        /// Gets the unique key for the schedule entity.
        /// </summary>
        public override object Key => ScheduleId;
    }
}
