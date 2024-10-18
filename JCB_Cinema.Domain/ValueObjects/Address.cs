namespace JCB_Cinema.Domain.ValueObjects
{
    public class Address : IEquatable<Address>
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string postalCode, string country)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        // Immutability - no setters, new addresses must be created whenever a change occurs
        public override bool Equals(object? obj) // still allowing null to maintain compatibility with Object method
        {
            if (obj is null) return false; // if obj is null, return false
            if (ReferenceEquals(this, obj)) return true; // reference comparison
            if (obj.GetType() != this.GetType()) return false; // type comparison

            var other = (Address)obj;
            return Street == other.Street &&
                   City == other.City &&
                   PostalCode == other.PostalCode &&
                   Country == other.Country; // property comparison
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, PostalCode, Country);
        }

        public bool Equals(Address? other)
        {
            return Equals((object?)other);
        }
    }
}
