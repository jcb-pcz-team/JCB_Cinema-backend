using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace JCB_Cinema.Tools
{
    public class EnumDescriptionSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumType = context.Type;

                // Zmieniamy typ na string, aby Swagger oczekiwał wartości tekstowych
                schema.Type = "string";
                schema.Format = null; // Usuń format, bo jest niepotrzebny dla string

                // Wyczyść bieżące wartości enum
                schema.Enum.Clear();

                // Dodaj wartości jako stringi z opisami
                foreach (var enumValue in Enum.GetValues(enumType).Cast<Enum>())
                {
                    var description = enumType.GetField(enumValue.ToString())
                        ?.GetCustomAttribute<DescriptionAttribute>()?.Description;

                    schema.Enum.Add(new OpenApiString(description ?? enumValue.ToString()));
                }
            }
        }
    }
}
