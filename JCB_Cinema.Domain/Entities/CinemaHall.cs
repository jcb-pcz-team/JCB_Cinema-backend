using System.ComponentModel.DataAnnotations.Schema;

namespace JCB_Cinema.Domain.Entities
{
    /// <summary>
    /// Represents a cinema hall within the cinema system.
    /// </summary>
    public class CinemaHall : EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cinema hall.
        /// </summary>
        public int CinemaHallId { get; set; }

        /// <summary>
        /// Gets or sets the name of the cinema hall.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of seats available in the cinema hall.
        /// </summary>
        public List<Seat> Seats { get; set; } = new List<Seat>();

        /// <summary>
        /// Gets the total number of seats in the cinema hall.
        /// This property is not mapped to the database.
        /// </summary>
        [NotMapped]
        public int? TotalSeats => Seats.Count;

        /// <summary>
        /// Gets the primary key of the cinema hall.
        /// This overrides the <see cref="EntityBase.Key"/> property.
        /// </summary>
        public override object Key => CinemaHallId;
    }
}
