using Petroineos.PowerServiceImpl;
using System;
using System.Collections.Specialized;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Petroineos.PositionExporterService
{
    public class PositionExporterService : ServiceBase
    {
        Timer _timer;
        NameValueCollection _appSettings;
        static IHost _host;
        IDateProvider _dateProvider;
        IPostionProvider _positionProvider;
        IPositionExporter _exporter;
        DateFormatter _dateFormatter;
        ILogger<PositionExporterService> _logger;

        static PositionExporterService()
        {
            
        }

        public PositionExporterService(IDateProvider dateProvider, IPositionExporter positionExporter, IPostionProvider postionProvider, DateFormatter dateFormatter, ILogger<PositionExporterService> logger)
        {
            base.ServiceName = "PositionExporterService";
            _dateProvider = dateProvider;
            _positionProvider = postionProvider;
            _exporter = positionExporter;
            _dateFormatter = dateFormatter;
            _logger = logger;
        }

        protected async override void OnStart(string[] args)
        {
            _logger.LogInformation("On-PostionExporterService-start");
            _appSettings = System.Configuration.ConfigurationManager.AppSettings;
            var interval = int.Parse(_appSettings.Get("Interval"));

            _logger.LogInformation($"Interval:${interval}");
            _timer = new Timer(interval);
            _timer.AutoReset = true;
            _timer.Elapsed += Handler;
            _timer.Start();
            await ExportPositions();
        }

        private async void Handler(object sender, ElapsedEventArgs e)
        {
            await ExportPositions();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _timer.Elapsed -= Handler;
        }

        private async Task ExportPositions()
        {
            try
            {
                Console.WriteLine("Constructing objects");
                var positions = await _positionProvider.GetPositionAsync(_dateProvider);
                Console.WriteLine("trades fetched");
                await _exporter.ExportAsync(positions, $"{_dateFormatter.GetDateForCSVName(_dateProvider)}.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static void Main()
        {
            _host = ServiceInitializer.Initialize();
            ServiceBase service = _host.Services.GetService<PositionExporterService>();
            Console.WriteLine("Service Instantiated successfully");
            ServiceBase.Run(service);
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            _host.Dispose();
        }

        private void InitializeComponent()
        {
            // 
            // PositionExporterService
            // 
            this.ServiceName = "PositionExporterService";

        }
    }
}
