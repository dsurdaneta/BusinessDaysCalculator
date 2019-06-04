using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.FileReaders;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class CalculatorTest
    {
        private Mock<IFileReadingManager> mockFileReadingManager;
        private FilePathInfo path = new FilePathInfo();

        public CalculatorTest()
        {
            this.mockFileReadingManager = new Mock<IFileReadingManager>();
        }

        [Fact]
        public void BusinessDays_CalculatorObjIsNotNull()
        {
            //Act
            var sut = new BusinessDaysCalculator();

            //Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_When_FirstParameterIsNull_Then_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(
                () => new BusinessDaysCalculator(null, this.mockFileReadingManager.Object));
        }
        
        [Fact(Skip ="Has to be fixed after Calculator refactor")]        
        public void BusinessDays_GetBusinessDaysCountNoHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 5, 26);
            var expectedDate = new DateTime(year, 6, 11);
            var calculator = new BusinessDaysCalculator();
            //Act
            //var sut = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate);
            ////Assert
            //Assert.AreEqual(10, sut);
        }

        [Fact(Skip = "Has to be fixed after Calculator refactor")]
        public void BusinessDays_AddBusinessDaysNoHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 5, 26);
            var expectedDate = new DateTime(year, 6, 15);
            
            //Act
            //var sut = BusinessDaysCalculator.AddBusinessDays(startDate, 15);
            ////Assert
            //Assert.AreEqual(expectedDate, sut);
        }

        [Fact(Skip = "Has to be fixed after Calculator refactor")]
        public void BusinessDays_AddBusinessDaysWithHolidaysCounter()
        {
            //Arrange
            var starDate = new DateTime(2001, 5, 26);
            var expectedDate = new DateTime(2001, 6, 18);
            
            //Act
            //var sut = BusinessDaysCalculator.AddBusinessDays(starDate, 15, 3);
            ////Assert
            //Assert.AreEqual(expectedDate, sut);
        }

        [Fact(Skip = "Has to be fixed after Calculator refactor")]
        public void BusinessDays_AddBusinessDaysFromList()
        {
            //Arrange
            const int year = 2001;
            var holidays = new List<Holiday>();
            var starDate = new DateTime(year, 4, 27);
            var expectedDate = new DateTime(year, 5, 17);
            
            //Act
            //holidays.Add(HolidayTests.GenerateHoliday(year));
            //var sut = BusinessDaysCalculator.AddBusinessDays(starDate, 15, holidays);
            ////Assert
            //Assert.AreEqual(expectedDate, sut);
        }

        [Fact(Skip = "Has to be fixed after Calculator refactor")]
        public void BusinessDays_GetBusinessDaysCountFromList()
        {
            //Arrange
            const int year = 2001;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 25);
            var expectedDate = new DateTime(year, 5, 9);
            
            //Act
            //holidays.Add(HolidayTests.GenerateHoliday(year));
            //var sut = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate, holidays);
            ////Assert
            //Assert.AreEqual(9, sut);
        }
    }
}
