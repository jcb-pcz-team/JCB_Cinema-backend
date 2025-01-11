namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents a range of dates with a start and end date.
    /// Implements <see cref="IEquatable{DateRange}"/> to support value equality comparisons.
    /// </summary>
    public class DateRange : IEquatable<DateRange>
    {
        /// <summary>
        /// Gets the start date of the range.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the end date of the range.
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="startDate">The start date of the range. Cannot be in the future relative to <paramref name="endDate"/>.</param>
        /// <param name="endDate">The end date of the range. Must be greater than <paramref name="startDate"/>.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="endDate"/> is less than or equal to the <paramref name="startDate"/>.
        /// </exception>
        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                throw new ArgumentException("End date must be greater than start date.");

            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Determines whether the specified date is within the date range.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns><c>true</c> if the specified <paramref name="date"/> is within the range; otherwise, <c>false</c>.</returns>
        public bool Contains(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is an instance of <see cref="DateRange"/> and has the same <see cref="StartDate"/> and <see cref="EndDate"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            var other = (DateRange)obj;
            return StartDate == other.StartDate && EndDate == other.EndDate;
        }

        /// <summary>
        /// Returns a hash code for the current <see cref="DateRange"/> object.
        /// </summary>
        /// <returns>A hash code that represents the current date range.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(StartDate, EndDate);
        }

        /// <summary>
        /// Determines whether the specified <see cref="DateRange"/> is equal to the current <see cref="DateRange"/>.
        /// </summary>
        /// <param name="other">The <see cref="DateRange"/> to compare with the current object.</param>
        /// <returns><c>true</c> if the two date ranges are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(DateRange? other)
        {
            return Equals((object?)other);
        }
    }
}
