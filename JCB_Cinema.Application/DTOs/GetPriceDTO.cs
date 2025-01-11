namespace JCB_Cinema.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the price details for a movie or service.
    /// </summary>
    public class GetPriceDTO
    {
        /// <summary>
        /// Gets or sets the amount of the price.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> representing the price amount.
        /// </value>
        public int Ammount { get; set; }

        /// <summary>
        /// Gets or sets the currency of the price.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the currency code (e.g., USD, EUR).
        /// This property is initialized with a non-nullable value.
        /// </value>
        public string Currency { get; set; } = null!;
    }
}
