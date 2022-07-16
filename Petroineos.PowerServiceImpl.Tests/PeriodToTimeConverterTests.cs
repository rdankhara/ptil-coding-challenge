using NUnit.Framework;

namespace Petroineos.PowerServiceImpl.Tests
{
    [TestFixture]
    class PeriodToTimeConverterTests
    {
        [TestCase(1, "23:00")]
        [TestCase(2, "00:00")]
        [TestCase(3, "01:00")]
        [TestCase(24, "22:00")]
        public void It_Convertes_Period_To_LocalTime_AsExpected(int period, string expectedTime)
        {
            string localTime = PeriodToTimeConverter.Convert(period);
            Assert.That(localTime, Is.EqualTo(expectedTime));
        }
    }
}
