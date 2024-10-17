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

        public override bool Equals(object? obj) // nadal dopuszczamy null, aby była zgodność z metodą Object
        {
            if (obj is null) return false; // jeśli obj jest null, zwracamy false
            if (ReferenceEquals(this, obj)) return true; // porównanie referencji
            if (obj.GetType() != this.GetType()) return false; // porównanie typów

            var other = (Price)obj; // rzutowanie na typ Money
            return AmountInCents == other.AmountInCents && Currency == other.Currency; // porównanie właściwości
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
