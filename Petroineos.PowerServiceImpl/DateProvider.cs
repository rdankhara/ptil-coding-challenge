using System;

namespace Petroineos.PowerServiceImpl
{
    public class DateProvider : IDateProvider
    {
        DateTime lastUsedDate;

        public DateTime GetDate()
        {
            lastUsedDate = DateTime.Now;
            return lastUsedDate;
        }

        public DateTime LastUsedDate()
        {
            return lastUsedDate;
        }
    }
}
