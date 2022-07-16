using NUnit.Framework;
using Services;
using System.Configuration;
using System.IO;

namespace Petroineos.PowerServiceImpl.Tests
{
    [TestFixture]
    public class ExporterIntegrationTest
    {
        [Test()]
        [Category("Integration")]
        public void VerifyExtractionAndExport_Integration()
        {
            var dateProvider = new DateProvider();

            var positions = new PositionProvider(new PowerService()).GetPosition(dateProvider);
            var configurationProvider = new ConfigurationProvider(ConfigurationManager.AppSettings);
            var exporter = new PositionExporter(configurationProvider);
            var filename = $"{new DateFormatter().GetDateForCSVName(dateProvider)}.csv";
            exporter.Export(positions, filename);

            Directory.Delete(configurationProvider.FileStoreLocation);
            var fileLocation = Path.Combine(configurationProvider.FileStoreLocation, filename);
            Assert.That(File.Exists(fileLocation));
        }
    }
}
