namespace JCB_Cinema.Domain.ValueObjects
{
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative");
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new ArgumentException("Cannot add different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        public override bool Equals(object? obj) // nadal dopuszczamy null, aby była zgodność z metodą Object
        {
            if (obj is null) return false; // jeśli obj jest null, zwracamy false
            if (ReferenceEquals(this, obj)) return true; // porównanie referencji
            if (obj.GetType() != this.GetType()) return false; // porównanie typów

            var other = (Money)obj; // rzutowanie na typ Money
            return Amount == other.Amount && Currency == other.Currency; // porównanie właściwości
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public bool Equals(Money? other)
        {
            return Equals((object?)other);
        }
    }
}
