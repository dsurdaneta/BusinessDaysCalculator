﻿using System;
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
        public void CountBusinessDays_When_ValidDatesWithoutHolidays_Then_Success()
        {
            // Arrange
            const int year = 2018;

            double expectedCount = 10;
            var holidays = new List<Holiday>();
            var calc = this.SetupCalculator(holidays);

            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 5, 9);
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

        [Fact]
        public void CountBusinessDays_When_EndDateIsOlderWithValidHolidays_Then_ReturnsZero()
        {
            // Arrange
            const int year = 2015;
            const int amount = 9;

            double expected = 0;
            var holidays = HolidayGenerator.CreateRandomHolidays(amount, year);
            var calc = this.SetupCalculator(new List<Holiday>());

            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 1, 9);

            // Act
            var sut = calc.CountBusinessDays(startDate, endDate, holidays);

            // Assert
            sut.Should().Be(expected);
        }

        [Fact]
        public async void AddBusinessDaysAsync_When_NegativeDaysCount_Then_ReturnsStartDate()
        {
            // Arrange
            const int daysCount = -32;
            var calc = this.SetupCalculator();
            var startDate = new DateTime(2003, 2, 15);

            // Act
            var sut = await calc.AddBusinessDaysAsync(startDate, daysCount).ConfigureAwait(false);

            // Assert
            sut.Should().Be(startDate);
        }
        
        [Fact]
        public async void AddBusinessDaysAsync_When_NoHolidays_Then_Success()
        {
            // Arrange
            const int daysCount = 12;

            var startDate = new DateTime(2003, 7, 3);
            var expectedDate = new DateTime(2003, 7, 19);

            var calc = this.SetupCalculator(new List<Holiday>());

            // Act
            var sut = await calc.AddBusinessDaysAsync(startDate, daysCount).ConfigureAwait(false);

            // Assert
            sut.Should().Be(expectedDate);
        }
        
        [Fact]
        public async void AddBusinessDaysAsync_When_ValidHolidays_Then_Success()
        {
            // Arrange
            const int daysCount = 20;
            const int year = 2016;

            var startDate = new DateTime(year, 6, 18);
            var expectedDate = new DateTime(year, 7, 17);

            var holidays = new List<Holiday>
            {
                HolidayGenerator.CreateHoliday(year, id: 3, month: 6,  day: 24, name: "Some Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 3, name: "Weekend Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 4, name: "Other Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 5, name: "Wow! another Holiday")
            };
            var calc = this.SetupCalculator(holidays);

            // Act
            var sut = await calc.AddBusinessDaysAsync(startDate, daysCount).ConfigureAwait(false);

            // Assert
            sut.Should().Be(expectedDate);
        }

        
        [Fact]
        public void AddBusinessDays_When_NoHolidaysCount_Then_Success()
        {
            // Arrange
            const int daysCount = 5;
            const int notWeekendHolidaysCount = -2;
            var startDate = new DateTime(1991, 3, 1);
            var expectedDate = new DateTime(1991, 3, 8);
            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);

            // Assert
            sut.Should().Be(expectedDate);
        }
        
        [Fact]
        public void AddBusinessDays_When_PositiveHolidaysCount_Then_Success()
        {
            // Arrange
            const int daysCount = 3;
            const int notWeekendHolidaysCount = 2;
            var startDate = new DateTime(1941, 3, 1);
            var expectedDate = new DateTime(1941, 3, 8);
            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);

            // Assert
            sut.Should().Be(expectedDate);
        }
        
        [Fact]
        public void AddBusinessDays_When_PositiveHolidaysCountButNoDayscount_Then_ReturnsStartDate()
        {
            // Arrange
            const int daysCount = 0;
            const int notWeekendHolidaysCount = 1;
            var startDate = new DateTime(1995, 1, 1);
            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, notWeekendHolidaysCount);

            // Assert
            sut.Should().Be(startDate);
        }
        
        [Fact]
        public void AddBusinessDays_When_ValidHolidaysButNoDaysCount_Then_ReturnsStartDate()
        {
            // Arrange
            const int daysCount = 0;
            var startDate = new DateTime(1983, 1, 1);
            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, new List<Holiday>());

            // Assert
            sut.Should().Be(startDate);
        }  
        
        [Fact]
        public void AddBusinessDays_When_NullHolidays_Then_ThrowsException()
        {
            // Arrange
            var calc = SetupCalculator();

            // Act
            Action action = () => calc.AddBusinessDays(new DateTime(),1, null);

            action.Should().Throw<ArgumentNullException>();
        } 
        
        [Fact]
        public void AddBusinessDays_When_PositiveDaysCountButNoHolidays_Then_Success()
        {
            // Arrange
            const int daysCount = 7;
            var startDate = new DateTime(2013, 2, 1);
            var expectedDate = new DateTime(2013, 2, 10);
            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, new List<Holiday>());

            // Assert
            sut.Should().Be(expectedDate);
        }
        
        [Fact]
        public void AddBusinessDays_When_PositiveDaysCountWithValidHolidays_Then_Success()
        {
            // Arrange
            const int daysCount = 20;
            const int year = 2016;

            var startDate = new DateTime(year, 6, 18);
            var expectedDate = new DateTime(year, 7, 17);

            var holidays = new List<Holiday>
            {
                HolidayGenerator.CreateHoliday(year, id: 3, month: 6,  day: 24, name: "Some Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 3, name: "Weekend Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 4, name: "Other Holiday"),
                HolidayGenerator.CreateHoliday(year, id: 3, month: 7,  day: 5, name: "Wow! another Holiday")
            };

            var calc = SetupCalculator();

            // Act
            var sut = calc.AddBusinessDays(startDate, daysCount, holidays);

            // Assert
            sut.Should().Be(expectedDate);
        } 

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
