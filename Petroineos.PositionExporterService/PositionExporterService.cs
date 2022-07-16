using Petroineos.PowerServiceImpl;
using Services;
using System;
using System.Collections.Specialized;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace Petroineos.PositionExporterService
{
    public class PositionExporterService : ServiceBase
    {
        Timer timer;
        NameValueCollection appSettings;
        public PositionExporterService()
        {
            base.ServiceName = "PositionExporterService";
        }

        protected async override void OnStart(string[] args)
        {
            Console.WriteLine("On-PostionExporterService-start");
            appSettings = System.Configuration.ConfigurationManager.AppSettings;
            var interval = int.Parse(appSettings.Get("Interval"));
            Console.WriteLine($"Interval:${interval}");
            timer = new Timer(interval);
            timer.AutoReset = true;
            timer.Elapsed += Handler;
            timer.Start();
            await ExportPositions();
        }

        private async void Handler(object sender, ElapsedEventArgs e)
        {
            await ExportPositions();
        }

        protected override void OnStop()
        {
            base.OnStop();
            timer.Elapsed -= Handler;
        }

        private async Task ExportPositions()
        {
            try
            {
                Console.WriteLine("Constructing objects");
                IDateProvider dateProvider = new DateProvider();
                IPostionProvider positionProvider = new PositionProvider(new PowerService());
                IConfigurationProvider configurationProvider = new ConfigurationProvider(appSettings);
                IPositionExporter exporter = new PositionExporter(configurationProvider);
                var formatter = new DateFormatter();
                Console.WriteLine("objects constructed fetching trades");
                var positions = await positionProvider.GetPositionAsync(dateProvider);
                Console.WriteLine("trades fetched");
                await exporter.ExportAsync(positions, $"{formatter.GetDateForCSVName(dateProvider)}.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static void Main()
        {
            ServiceBase.Run(new PositionExporterService());
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
