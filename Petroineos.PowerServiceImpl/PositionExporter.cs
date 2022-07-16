using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public class PositionExporter : IPositionExporter
    {
        private readonly IConfigurationProvider configurationProvider;

        public PositionExporter(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        private string buildFileLocation(string filename)
        {
            string fileLocation = Path.Combine(configurationProvider.FileStoreLocation, filename);
            if (!Directory.Exists(configurationProvider.FileStoreLocation))
            {
                Directory.CreateDirectory(configurationProvider.FileStoreLocation);
            }

            return fileLocation;

        }
        public void Export(IEnumerable<Position> positions, string fileName)
        {

            using (StreamWriter sw = new StreamWriter(buildFileLocation(fileName)))
            {
                foreach (Position position in positions)
                {
                    sw.WriteLine($"{position.LocalTime},{position.Volume}");
                }
            }
        }

        public Task ExportAsync(IEnumerable<Position> positions, string fileName)
        {
            return Task.Run(() => Export(positions, fileName));
        }
    }
}
