using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmpujeComunitario.MessageFlow.Api.Configuration
{
    public class ApplySwaggerHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // 1. Busca el SwaggerHeaderAttribute en el método del endpoint
            var swaggerHeaderAttributes = context.MethodInfo.GetCustomAttributes(true)
                .OfType<SwaggerHeaderAttribute>();

            // 2. Itera sobre cada Attribute encontrado
            foreach (var attribute in swaggerHeaderAttributes)
            {
                // 3. Crea una instancia del Operation Filter definido en el Attribute
                var operationFilter = (IOperationFilter)Activator.CreateInstance(attribute.Type, attribute.Arguments);

                // 4. Aplica el filtro a la operación actual
                operationFilter.Apply(operation, context);
            }
        }
    }
}
