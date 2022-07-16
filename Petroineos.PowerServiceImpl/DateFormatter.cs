namespace Petroineos.PowerServiceImpl
{
    public class DateFormatter
    {
        public string GetDateForCSVName(IDateProvider dateProvider)
        {
            return dateProvider.GetDate().ToString("yyyyMMdd_HHmm");
        }
    }
}
