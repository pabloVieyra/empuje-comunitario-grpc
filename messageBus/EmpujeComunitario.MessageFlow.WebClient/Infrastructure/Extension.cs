using EmpujeComunitario.MessageFlow.WebClient.Setting;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace EmpujeComunitario.MessageFlow.WebClient.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddConfigurationMessageFlow(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var options = configuration
                 .GetSection("ExternalServices:MessageFlow")
                 .Get<SettingModel>() ?? new SettingModel();

            return services
                .AddConfigurationWebClient(options);
        }

        
    
    }
}
