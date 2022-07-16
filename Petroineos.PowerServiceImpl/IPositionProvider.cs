using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public interface IPostionProvider
    {
        IEnumerable<Position> GetPosition(IDateProvider dateProvider);

        Task<IEnumerable<Position>> GetPositionAsync(IDateProvider dateProvider);
    }
}