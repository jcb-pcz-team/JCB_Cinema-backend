namespace JCB_Cinema.Domain.ValueObjects
{
    public class Price : IEquatable<Price>
    {
        public int AmountInCents { get; }
        public string Currency { get; }

        public Price(int amountInCents, string currency)
        {
            if (amountInCents < 0) throw new ArgumentException("Amount cannot be negative");
            AmountInCents = amountInCents;
            Currency = (currency ?? throw new ArgumentNullException(nameof(currency))).ToLower();
        }

        public Price()
        {
            AmountInCents = 0;
            Currency = "";
        }

        public Price Add(Price other)
        {
            if (Currency != other.Currency)
                throw new ArgumentException("Cannot add different currencies.");

            return new Price(AmountInCents + other.AmountInCents, Currency);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            var other = (Price)obj;
            return AmountInCents == other.AmountInCents && Currency == other.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AmountInCents, Currency);
        }

        public bool Equals(Price? other)
        {
            return Equals((object?)other);
        }
    }
}
