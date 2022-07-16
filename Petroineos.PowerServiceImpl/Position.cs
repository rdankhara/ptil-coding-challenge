namespace Petroineos.PowerServiceImpl
{
    public class Position
    {
        public Position(int period, double volume)
        {
            LocalTime = PeriodToTimeConverter.Convert(period);
            Volume = volume;
        }

        public string LocalTime { get; }
        public double Volume { get; }
    }
}
