namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents a physical address with street, city, postal code, and country.
    /// Implements <see cref="IEquatable{Address}"/> to support value equality comparisons.
    /// </summary>
    public class Address : IEquatable<Address>
    {
        /// <summary>
        /// Gets the street address.
        /// </summary>
        public string Street { get; }

        /// <summary>
        /// Gets the city of the address.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets the postal code of the address.
        /// </summary>
        public string PostalCode { get; }

        /// <summary>
        /// Gets the country of the address.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street of the address. Cannot be null.</param>
        /// <param name="city">The city of the address. Cannot be null.</param>
        /// <param name="postalCode">The postal code of the address. Cannot be null.</param>
        /// <param name="country">The country of the address. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the arguments <paramref name="street"/>, <paramref name="city"/>, <paramref name="postalCode"/>, or <paramref name="country"/> are null.
        /// </exception>
        public Address(string street, string city, string postalCode, string country)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is an instance of <see cref="Address"/> and has the same values for <see cref="Street"/>, <see cref="City"/>, <see cref="PostalCode"/>, and <see cref="Country"/>; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            var other = (Address)obj;
            return Street == other.Street &&
                   City == other.City &&
                   PostalCode == other.PostalCode &&
                   Country == other.Country;
        }

        /// <summary>
        /// Returns a hash code for the current <see cref="Address"/> object.
        /// </summary>
        /// <returns>A hash code that represents the current address.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, PostalCode, Country);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Address"/> is equal to the current <see cref="Address"/>.
        /// </summary>
        /// <param name="other">The <see cref="Address"/> to compare with the current object.</param>
        /// <returns><c>true</c> if the two addresses are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Address? other)
        {
            return Equals((object?)other);
        }
    }
}
