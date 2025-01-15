using System.ComponentModel;

namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Enum representing the status of a seat in the cinema system.
    /// </summary>
    public enum SeatStatus
    {
        /// <summary>
        /// Indicates that the seat is available for booking.
        /// </summary>
        [Description("Available")]
        Available,

        /// <summary>
        /// Indicates that the seat is currently occupied by a booking.
        /// </summary>
        [Description("Occupied")]
        Occupied,

        /// <summary>
        /// Indicates that the seat is reserved but not yet confirmed.
        /// </summary>
        [Description("Reservation")]
        Reservation
    }
}
