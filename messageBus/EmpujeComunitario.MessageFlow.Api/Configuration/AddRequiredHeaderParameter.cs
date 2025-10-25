using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmpujeComunitario.MessageFlow.Api.Configuration
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        private readonly string _headerName;

        public AddRequiredHeaderParameter(string headerName)
        {
            _headerName = headerName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Solo agregamos si no existe para evitar duplicados
            if (!operation.Parameters.Any(p => p.Name == _headerName && p.In == ParameterLocation.Header))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = _headerName,
                    In = ParameterLocation.Header,
                    Description = "ID del usuario requerido para la operación.",
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" }
                });
            }
        }
    }
}