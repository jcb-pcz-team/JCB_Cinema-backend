namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with an amount in cents and a specified currency.
    /// </summary>
    public class Price : IEquatable<Price>
    {
        /// <summary>
        /// Gets the amount in cents.
        /// </summary>
        public int AmountInCents { get; private set; }

        /// <summary>
        /// Gets the currency in lowercase (e.g., "pln", "usd").
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class with a specified amount and currency.
        /// </summary>
        /// <param name="amountInCents">The amount in cents. Must be non-negative.</param>
        /// <param name="currency">The currency code (e.g., "pln"). Cannot be null.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="amountInCents"/> is negative.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="currency"/> is null.</exception>
        public Price(int amountInCents, string currency)
        {
            if (amountInCents < 0) throw new ArgumentException("Amount cannot be negative");
            AmountInCents = amountInCents;
            Currency = (currency ?? throw new ArgumentNullException(nameof(currency))).ToLower();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class with a default value of 0.
        /// </summary>
        public Price()
        {
            AmountInCents = 0;
            Currency = string.Empty;
        }

        /// <summary>
        /// Adds the current price to another price.
        /// </summary>
        /// <param name="other">The price to add.</param>
        /// <returns>A new <see cref="Price"/> instance that is the sum of the two prices.</returns>
        /// <exception cref="ArgumentException">Thrown if the currencies of the two prices do not match.</exception>
        public Price Add(Price other)
        {
            if (Currency != other.Currency)
                throw new ArgumentException("Cannot add different currencies.");

            return new Price(AmountInCents + other.AmountInCents, Currency);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Price"/> is equal to the current <see cref="Price"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="Price"/>.</param>
        /// <returns><c>true</c> if the specified object is equal to the current <see cref="Price"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            var other = (Price)obj;
            return AmountInCents == other.AmountInCents && Currency == other.Currency;
        }

        /// <summary>
        /// Returns a hash code for the current <see cref="Price"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Price"/>.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(AmountInCents, Currency);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Price"/> is equal to the current <see cref="Price"/>.
        /// </summary>
        /// <param name="other">The <see cref="Price"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="Price"/> is equal to the current <see cref="Price"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Price? other)
        {
            return Equals((object?)other);
        }
    }
}
