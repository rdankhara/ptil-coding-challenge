using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public interface IPositionExporter
    {
        void Export(IEnumerable<Position> positions, string fileName);
        Task ExportAsync(IEnumerable<Position> positions, string fileName);
    }
}