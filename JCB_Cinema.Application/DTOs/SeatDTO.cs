using JCB_Cinema.Domain.ValueObjects;

namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a seat.
    /// </summary>
    public class SeatDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the seat.
        /// </summary>
        /// <value>The ID of the seat.</value>
        public int SeatId { get; set; }

        /// <summary>
        /// Gets or sets the status of the seat.
        /// </summary>
        /// <value>The current status of the seat, represented by the <see cref="SeatStatus"/> enumeration.</value>
        public SeatStatus Status { get; set; }
    }
}
