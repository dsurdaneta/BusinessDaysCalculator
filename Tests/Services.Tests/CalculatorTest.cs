using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using DsuDev.BusinessDays.Tools.SampleGenerators;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class CalculatorTest
    {
        private readonly Mock<IFileReadingManager> mockFileReadingManager;
        private FilePathInfo path;

        public CalculatorTest()
        {
            this.mockFileReadingManager = new Mock<IFileReadingManager>();
            this.path = new FilePathInfo();
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(FilePathInfo filePathInfo, IFileReadingManager fileReadingManager)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(
                () => new BusinessDaysCalculator(filePathInfo, fileReadingManager));
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
        public void BusinessDays_GetBusinessDaysCountNoHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 5, 26);
            var expectedDate = new DateTime(year, 6, 11);
            var calculator = GetCalculator();

            //Act
            var sut = calculator.GetBusinessDaysCount(startDate, expectedDate);

            //Assert
            sut.Should().Be(10);
        }

        [Fact]
        public void BusinessDays_AddBusinessDaysNoHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 5, 26);
            var expectedDate = new DateTime(year, 6, 15);
            var calculator = GetCalculator();

            //Act
            var sut = calculator.AddBusinessDays(startDate, 15);

            //Assert
            sut.Should().Be(expectedDate);
        }

        [Fact]
        public void BusinessDays_AddBusinessDaysWithHolidaysCounter()
        {
            //Arrange
            var starDate = new DateTime(2001, 5, 26);
            var expectedDate = new DateTime(2001, 6, 18);
            var calculator = GetCalculator();

            //Act
            var sut = calculator.AddBusinessDays(starDate, 15, 3);

            //Assert
            sut.Should().Be(expectedDate);
        }

        [Fact]
        public void BusinessDays_AddBusinessDaysFromList()
        {
            //Arrange
            const int year = 2001;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 27);
            var expectedDate = new DateTime(year, 5, 17);
            var calculator = GetCalculator();

            //Act
            holidays.Add(HolidayGenerator.CreateHoliday());
            var sut = calculator.AddBusinessDays(startDate, 15, holidays);

            //Assert
            sut.Should().Be(expectedDate);
        }

        [Fact]
        public void BusinessDays_GetBusinessDaysCountFromList()
        {
            //Arrange
            const int year = 2009;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 5, 9);
            var calculator = GetCalculator();

            //Act
            holidays.Add(HolidayGenerator.CreateHoliday(year: year));
            var sut = calculator.GetBusinessDaysCount(startDate, endDate, holidays);

            //Assert
            sut.Should().Be(9);
        }
        
        private BusinessDaysCalculator GetCalculator()
        {
            this.mockFileReadingManager.Setup(x => x.ReadHolidaysFile(It.IsAny<FilePathInfo>())).Returns(new List<Holiday>());

            var calculator = new BusinessDaysCalculator(this.path, this.mockFileReadingManager.Object);
            return calculator;
        }
    }
}
