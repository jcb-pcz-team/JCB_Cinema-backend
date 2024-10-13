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
    }
}
