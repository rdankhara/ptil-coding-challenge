using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Petroineos.PowerServiceImpl;

namespace Petroineos.PositionExporterService
{
    public class ServiceInitializer
    {
        public static IHost Initialize()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                    {
                        services
                            .AddLogging();

                        Bootstrapper.Initialize(services, System.Configuration.ConfigurationManager.AppSettings);

                        services.AddSingleton<PositionExporterService>();
                    }
                ).Build();

            return host;
        }
    }
}
