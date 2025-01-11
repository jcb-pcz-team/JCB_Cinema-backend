using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace JCB_Cinema.Tools
{
    /// <summary>
    /// A schema filter to modify the Swagger UI for enum types by replacing their values with the associated descriptions.
    /// Implements <see cref="ISchemaFilter"/>.
    /// </summary>
    public class EnumDescriptionSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies custom modifications to the OpenAPI schema for enum types.
        /// The enum values are converted into their corresponding descriptions, which are displayed in Swagger UI.
        /// </summary>
        /// <param name="schema">The OpenAPI schema to apply the filter to.</param>
        /// <param name="context">The context that contains information about the type being processed.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Check if the type is an enum
            if (context.Type.IsEnum)
            {
                var enumType = context.Type;

                // Change the type to string so that Swagger expects text values
                schema.Type = "string";
                schema.Format = null; // Remove the format, as it's not necessary for strings

                // Clear the existing enum values
                schema.Enum.Clear();

                // Add enum values as strings with their descriptions
                foreach (var enumValue in Enum.GetValues(enumType).Cast<Enum>())
                {
                    // Get the description attribute of the enum value, if any
                    var description = enumType.GetField(enumValue.ToString())
                        ?.GetCustomAttribute<DescriptionAttribute>()?.Description;

                    // Add the description (or the enum value name if no description exists) to the schema
                    schema.Enum.Add(new OpenApiString(description ?? enumValue.ToString()));
                }
            }
        }
    }
}
