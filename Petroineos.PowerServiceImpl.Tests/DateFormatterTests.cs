using Moq;
using NUnit.Framework;
using System;

namespace Petroineos.PowerServiceImpl.Tests
{
    [TestFixture]
    public class DateFormatterTests
    {
        private Mock<IDateProvider> _mockDateProvider;
        private DateFormatter _sut;

        [SetUp]
        public void Setup()
        {
            _mockDateProvider = new Mock<IDateProvider>();
            _sut = new DateFormatter();
        }

        [TestCase(2022, 1, 1, 1, 1, "20220101_0101")]
        [TestCase(2022, 12, 20, 23, 59, "20221220_2359")]
        public void It_ConvertsTo_CSVFileName_Format(int year, int month, int date, int hours, int minutes, string expected)
        {
            //Arrange
            _mockDateProvider.Setup(x => x.GetDate()).Returns(new DateTime(year, month, date, hours, minutes, 1));

            //Act
            var formattedString = _sut.GetDateForCSVName(_mockDateProvider.Object);

            ///Assert
            Assert.That(formattedString, Is.EqualTo(expected));
        }
    }
}
