using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmpujeComunitario.Graphql.DataAccess.Context
{
    public class MessageFlowDbContextFactory : IDesignTimeDbContextFactory<MessageFlowDbContext>
    {
        public MessageFlowDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            // Asegúrate de que este path apunta al proyecto que tiene appsettings.json
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MessageFlowDbContext>();

            // Tomar la conexión de Postgres
            var connectionString = "Host=localhost:5435;Database=postgres;Username=postgres;Password=root";

            optionsBuilder.UseNpgsql(connectionString);

            return new MessageFlowDbContext(optionsBuilder.Options);
        }
    }
}
