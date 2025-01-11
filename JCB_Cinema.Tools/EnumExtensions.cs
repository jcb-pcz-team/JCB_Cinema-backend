using System.ComponentModel;
using System.Reflection;

namespace JCB_Cinema.Tools
{
    /// <summary>
    /// Extension methods for enums to retrieve descriptions and to convert descriptions back to enum values.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the description of an enum value using the <see cref="DescriptionAttribute"/>.
        /// If no description is provided, the enum's string representation is returned.
        /// </summary>
        /// <param name="value">The enum value whose description is to be retrieved.</param>
        /// <returns>The description of the enum value, or the enum value's string representation if no description is found.</returns>
        public static string GetDescription(this Enum value)
        {
            // Get the field corresponding to the given enum value
            FieldInfo? field = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            // Return the description if the attribute exists, otherwise return the enum's string value
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Converts a string description to its corresponding enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to convert the description to.</typeparam>
        /// <param name="description">The description to be converted into an enum value.</param>
        /// <returns>The enum value corresponding to the provided description.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the description is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when no matching enum value is found for the given description.</exception>
        public static TEnum GetValueFromDescription<TEnum>(string? description) where TEnum : Enum
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
            }

            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                FieldInfo? field = typeof(TEnum).GetField(value.ToString()!);
                DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null && string.Equals(attribute.Description, description, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)value;
                }

                if (attribute == null && string.Equals(value.ToString(), description, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)value;
                }
            }

            throw new ArgumentException($"No enum value found for description '{description}'", nameof(description));
        }
    }
}
