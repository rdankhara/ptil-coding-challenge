namespace Petroineos.PowerServiceImpl
{
    public static class PeriodToTimeConverter
    {
        public static string Convert(int period)
        {
            string hours = $"{(22 + period) % 24}".PadLeft(2, '0');
            var localTime = $"{hours}:00";
            return localTime;
        }
    }
}
