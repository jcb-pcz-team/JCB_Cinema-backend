using System.ComponentModel;
using System.Globalization;

namespace JCB_Cinema.Tools
{
    /// <summary>
    /// A custom type converter for enums that converts between the enum values and their associated descriptions.
    /// Inherits from <see cref="EnumConverter"/> to provide string-based conversion for enum types.
    /// </summary>
    /// <typeparam name="T">The enum type that the converter is applied to.</typeparam>
    public class EnumDescriptionTypeConverter<T> : EnumConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDescriptionTypeConverter{T}"/> class.
        /// </summary>
        public EnumDescriptionTypeConverter() : base(typeof(T)) { }

        /// <summary>
        /// Converts the specified enum value to its corresponding description string.
        /// If no description is found, it returns the enum value as a string.
        /// </summary>
        /// <param name="context">The context for type descriptor, or null.</param>
        /// <param name="culture">The culture info to use for conversion, or null.</param>
        /// <param name="value">The enum value to be converted.</param>
        /// <param name="destinationType">The destination type for the conversion, typically <see cref="string"/>.</param>
        /// <returns>The description of the enum value, or the enum value itself if no description is available.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null)
            {
                var fieldInfo = typeof(T).GetField(value.ToString());
                var descriptionAttributes = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (descriptionAttributes?.Length > 0)
                {
                    return descriptionAttributes[0].Description;
                }
                return value.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Converts a string description back to its corresponding enum value.
        /// </summary>
        /// <param name="context">The context for type descriptor, or null.</param>
        /// <param name="culture">The culture info to use for conversion, or null.</param>
        /// <param name="value">The string value representing the description of an enum.</param>
        /// <returns>The enum value corresponding to the provided description.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                foreach (var field in typeof(T).GetFields())
                {
                    var descriptionAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if (descriptionAttributes?.Length > 0 && descriptionAttributes[0].Description == stringValue)
                    {
                        return Enum.Parse(typeof(T), field.Name);
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
