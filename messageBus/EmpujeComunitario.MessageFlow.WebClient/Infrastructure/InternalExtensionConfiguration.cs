using EmpujeComunitario.MessageFlow.WebClient.Interface;
using EmpujeComunitario.MessageFlow.WebClient.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Refit;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace EmpujeComunitario.MessageFlow.WebClient.Infrastructure
{
    internal static class InternalExtensionConfiguration
    {
        internal static IServiceCollection AddConfigurationWebClient(this IServiceCollection services, SettingModel options)
        {
            services.AddRefitClient<IExternalDataClient>()
                 .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                 .ConfigureHttpClient(ConfigureHttpClient(options.EndpointConfiguration))
                 .ConfigureCertificateValidation();

            services.AddRefitClient<IMessagesPublisherClient>()
                 .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                 .ConfigureHttpClient(ConfigureHttpClient(options.EndpointConfiguration))
                 .ConfigureCertificateValidation();

            return services; 
        }
        private static Action<IServiceProvider, HttpClient> ConfigureHttpClient(string uri) =>
           (provider, client) =>
           {
               client.BaseAddress = new Uri(uri);
               client.Timeout = Timeout.InfiniteTimeSpan;
               var headers = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Request?.Headers.ToList();
               if (headers != null)
                   foreach (var H in headers)
                   {
                       client.DefaultRequestHeaders.TryAddWithoutValidation(H.Key, H.Value.ToString());
                   }
           };
        private static void ConfigureCertificateValidation(this IHttpClientBuilder builder)
        {
            builder.ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return LogicaValidacionCertificado(cert, chain, sslPolicyErrors);
                };

                return handler;
            });
        }

        private static bool LogicaValidacionCertificado(
            X509Certificate2 cert,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (!chain.Build(cert))
            {
                return false;
            }

            DateTime fechaActual = DateTime.Now;
            if (fechaActual < cert.NotBefore || fechaActual > cert.NotAfter)
            {
                return false;
            }

            return true;
        }
    }
}
