namespace JCB_Cinema.Domain.ValueObjects
{
    public class DateRange : IEquatable<DateRange>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                throw new ArgumentException("End date must be greater than start date.");

            StartDate = startDate;
            EndDate = endDate;
        }

        public bool Contains(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            var other = (DateRange)obj;
            return StartDate == other.StartDate && EndDate == other.EndDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartDate, EndDate);
        }

        public bool Equals(DateRange? other)
        {
            return Equals((object?)other);
        }
    }
}
