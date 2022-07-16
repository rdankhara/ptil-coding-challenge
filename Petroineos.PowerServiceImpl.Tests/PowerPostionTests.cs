using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl.Tests
{
    [TestFixture]
    public class PowerPostionTests
    {
        private Mock<IPowerService> _mockPowerService;
        private Mock<IDateProvider> _mockDateProvider;
        private Mock<ILogger<PositionProvider>> _mockPositionProviderLogger;
        public void MyTestMethod()
        {

        }

        [SetUp]
        public void Setup()
        {
            _mockDateProvider = new Mock<IDateProvider>();
            _mockDateProvider.Setup(x => x.GetDate()).Returns(DateTime.Now);
            _mockPowerService = new Mock<IPowerService>(MockBehavior.Default);
            _mockPositionProviderLogger = new Mock<ILogger<PositionProvider>>(MockBehavior.Default);
        }

        [Test]
        public void It_Aggregates_PowerService_Response_To_Position()
        {
            var dateProvider = _mockDateProvider.Object;
            var date = dateProvider.GetDate();
            var fakeResponse = FakePowerTrades.GetFakePowerTradesResponse(date);
            _mockPowerService.Setup(x => x.GetTrades(date)).Returns(fakeResponse);

            PositionProvider powerPosition = new PositionProvider(_mockPowerService.Object, _mockPositionProviderLogger.Object);
            var positions = powerPosition.GetPosition(dateProvider);

            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            foreach(var fakePeriod in fakeResponse)
            {
                foreach(var p in fakePeriod.Periods)
                {
                    double val;
                    if ( dictionary.TryGetValue(p.Period, out val))
                    {
                        dictionary[p.Period] = val + p.Volume;
                    } else
                    {
                        dictionary.Add(p.Period, p.Volume);
                    }
                }
            }
            Assert.That(positions.Count(), Is.EqualTo(dictionary.Count));
        }
    }
}
