using System.ComponentModel;
using System.Reflection;

namespace JCB_Cinema.Tools
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            // Pobierz pole dla danego enum
            FieldInfo? field = value.GetType().GetField(value.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            // Zwróć opis, jeśli atrybut istnieje, w przeciwnym razie zwróć stringową wartość enum
            return attribute == null ? value.ToString() : attribute.Description;
        }
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
