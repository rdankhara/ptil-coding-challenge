using Microsoft.Extensions.DependencyInjection;
using Services;
using System.Collections.Specialized;

namespace Petroineos.PowerServiceImpl
{
    public class Bootstrapper
    {
        public static void Initialize(IServiceCollection service, NameValueCollection appSettings)
        {
            service.AddSingleton<IDateProvider, DateProvider>()
                .AddSingleton<IPowerService, PowerService>()
                .AddSingleton<DateFormatter>()
                .AddSingleton<IConfigurationProvider>((s) => new ConfigurationProvider(appSettings))
                .AddSingleton<IPostionProvider, PositionProvider>()
                .AddSingleton<IPositionExporter, PositionExporter>();
        }
    }
}
