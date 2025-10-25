using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmpujeComunitario.Graphql.Api.Configuration
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        private readonly string _headerName;
        private readonly string _description;
        private readonly bool _required;

        // Constructor para configurar el nombre y la descripción del header
        public AddRequiredHeaderParameter(string headerName, string description, bool required = true)
        {
            _headerName = headerName;
            _description = description;
            _required = required;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Inicializa la lista de parámetros si es nula
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Agrega el header como un parámetro
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = _headerName,
                In = ParameterLocation.Header, // Indica que es un Header
                Description = _description,
                Required = _required, // Indica si es obligatorio o no
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
