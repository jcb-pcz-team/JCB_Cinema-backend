using System.ComponentModel;
using System.Globalization;

namespace JCB_Cinema.Tools
{
    public class EnumDescriptionTypeConverter<T> : EnumConverter
    {
        public EnumDescriptionTypeConverter() : base(typeof(T)) { }

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
