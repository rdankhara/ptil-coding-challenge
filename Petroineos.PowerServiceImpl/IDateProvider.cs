using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public interface IDateProvider
    {
        DateTime GetDate();
        DateTime LastUsedDate();
    }
}
