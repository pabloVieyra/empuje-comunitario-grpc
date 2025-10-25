namespace EmpujeComunitario.MessageFlow.Api.Configuration
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerHeaderAttribute : Attribute
    {
        public Type Type { get; }
        public object[] Arguments { get; }

        public SwaggerHeaderAttribute(string headerName)
        {
            // Guardamos el tipo de IOperationFilter y sus argumentos
            Type = typeof(AddRequiredHeaderParameter);
            Arguments = new object[] { headerName };
        }
    }
}
