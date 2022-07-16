using Microsoft.Extensions.Logging;
using Moq;
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
            Mock<ILogger<PositionProvider>> mockLogger = new Mock<ILogger<PositionProvider>>();
            Mock<ILogger<PositionExporter>> mockPositionExporterLogger = new Mock<ILogger<PositionExporter>>();

            var positions = new PositionProvider(new PowerService(), mockLogger.Object).GetPosition(dateProvider);
            var configurationProvider = new ConfigurationProvider(ConfigurationManager.AppSettings);
            var exporter = new PositionExporter(configurationProvider, mockPositionExporterLogger.Object);
            var filename = $"{new DateFormatter().GetDateForCSVName(dateProvider)}.csv";
            exporter.Export(positions, filename);

            var fileLocation = Path.Combine(configurationProvider.FileStoreLocation, filename);
            Assert.That(File.Exists(fileLocation));
        }
    }
}
