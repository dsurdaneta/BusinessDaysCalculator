using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class CalculatorTests
    {
        private Mock<IDataProvider> mockDataProvider;

        public CalculatorTests()
        {
            this.mockDataProvider = new Mock<IDataProvider>();
        }

        [Fact]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException()
        {
            // Act
            Action action = () => new Calculator(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        //TODO add CountBusinessDays tests
        [Fact]
        public void CountBusinessDays_When_EndDateIsOlder_Then_ReturnsZero()
        {
            // Arrange
            const int year = 2008;
            const int amount = 5;
            
            double expected = 0;
            var holidays = HolidayGenerator.CreateRandomHolidays(amount, year);
            var calc = this.SetupCalculator(holidays);

            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 1, 9);

            // Act
            var sut = calc.CountBusinessDays(startDate, endDate);

            // Assert
            sut.Should().Be(expected);
        }

        [Fact]
        public void CountBusinessDays_When_ValidDates_Then_Success()
        {
            // Arrange
            const int year = 2018;

            double expectedCount = 74;
            var holidays = new List<Holiday>
            {
                HolidayGenerator.CreateHoliday(year, id: 1),
                HolidayGenerator.CreateHoliday(year, id: 2, month: 4,  day: 29, name: "Weekend Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 3, name: "Some Holiday")
            };
            var calc = this.SetupCalculator(holidays);

            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 8, 9);
            var diff = (endDate.ToUniversalTime() - startDate.ToUniversalTime()).TotalDays;

            // Act
            var sut = calc.CountBusinessDays(startDate, endDate);

            // Assert
            sut.Should().Be(expectedCount);
            sut.Should().BeLessThan(diff);
        }

        [Fact]
        public void CountBusinessDays_When_NullHolidaysParameter_Then_ThrowsException()
        {
            // Arrange
            var calc = this.SetupCalculator(new List<Holiday>());

            // Act
            Action action = () => calc.CountBusinessDays(new DateTime(), DateTime.Today, null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CountBusinessDays_When_ValidHolidays_Then_Success()
        {
            // Arrange
            const int year = 2018;

            double expectedCount = 74;
            var holidays = new List<Holiday>
            {
                HolidayGenerator.CreateHoliday(year, id: 1),
                HolidayGenerator.CreateHoliday(year, id: 2, month: 4,  day: 29, name: "Weekend Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 3, name: "Some Holiday")
            };
            var calc = this.SetupCalculator();

            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 8, 9);
            var diff = (endDate.ToUniversalTime() - startDate.ToUniversalTime()).TotalDays;

            // Act
            var sut = calc.CountBusinessDays(startDate, endDate, holidays);

            // Assert
            sut.Should().Be(expectedCount);
            sut.Should().BeLessThan(diff);
        }

        //TODO add AddBusinessDaysAsync tests
        //TODO add AddBusinessDays tests

        private Calculator SetupCalculator(ICollection<Holiday> expectedHolidays = null)
        {
            this.mockDataProvider = new Mock<IDataProvider>();

            if (expectedHolidays != null)
            {
                this.mockDataProvider
                    .Setup(setup => setup.GetHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                    .Returns(expectedHolidays);

                 this.mockDataProvider
                    .Setup(setup => setup.GetAllHolidaysAsync())
                    .Returns(Task.FromResult(expectedHolidays));
            }

            return  new Calculator(this.mockDataProvider.Object);
        }
    }
}
