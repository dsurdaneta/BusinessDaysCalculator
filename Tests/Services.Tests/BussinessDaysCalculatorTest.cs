using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class BussinessDaysCalculatorTest
    {
        private readonly Mock<IFileReadingManager> mockFileReadingManager;
        private readonly FilePathInfo path;

        public BussinessDaysCalculatorTest()
        {
            this.mockFileReadingManager = new Mock<IFileReadingManager>();
            this.path = new FilePathInfo();
        }

        [Theory]
        [ClassData(typeof(BussinessDaysCalculatorTestData))]
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
        public void BusinessDays_GetBusinessDaysCount_NoHolidaysFile()
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
        public void BusinessDays_GetBusinessDaysCount_WithHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 4, 26);
            var expectedDate = new DateTime(year, 5, 11);
            var calculator = GetCalculator(year,true);

            //Act
            var sut = calculator.GetBusinessDaysCount(startDate, expectedDate, true);

            //Assert
            sut.Should().Be(10);
        }

        [Fact]
        public void BusinessDays_AddBusinessDays_NoHolidaysFile()
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
        public void BusinessDays_AddBusinessDays_NoHolidaysFileWithNegativeCount()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 5, 26);
            var calculator = GetCalculator();

            //Act
            var sut = calculator.AddBusinessDays(startDate, -5);

            //Assert
            sut.Should().Be(startDate);
        }
        
        [Fact]
        public void BusinessDays_AddBusinessDays_WithHolidaysFile()
        {
            //Arrange
            const int year = 2001;
            var startDate = new DateTime(year, 4, 26);
            var expectedDate = new DateTime(year, 5, 16);
            var calculator = GetCalculator(year,true);

            //Act
            var sut = calculator.AddBusinessDays(startDate, 15, true);

            //Assert
            sut.Should().Be(expectedDate);
        }

        [Fact]
        public void BusinessDays_AddBusinessDays_WithHolidaysCounter()
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
        public void BusinessDays_AddBusinessDays_WithNegativeHolidaysCounter()
        {
            //Arrange
            var starDate = new DateTime(2001, 5, 26);
            var calculator = GetCalculator();

            //Act
            var sut = calculator.AddBusinessDays(starDate, -15, -3);

            //Assert
            sut.Should().Be(starDate);
        }

        [Fact]
        public void BusinessDays_AddBusinessDaysFromList()
        {
            //Arrange
            const int year = 2001;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 27);
            var expectedDate = new DateTime(year, 5, 17);
            var calculator = GetCalculator(year);

            //Act
            holidays.Add(HolidayGenerator.CreateHoliday());
            var sut = calculator.AddBusinessDays(startDate, 15, holidays);

            //Assert
            sut.Should().Be(expectedDate);
        }
        
        [Fact]
        public void BusinessDays_AddBusinessDaysFromListWithNegativeCount()
        {
            //Arrange
            const int year = 2001;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 27);
            var calculator = GetCalculator(year);

            //Act
            holidays.Add(HolidayGenerator.CreateHoliday());
            var sut = calculator.AddBusinessDays(startDate, -5, holidays);

            //Assert
            sut.Should().Be(startDate);
        }

        [Fact]
        public void BusinessDays_GetBusinessDaysCountFromList()
        {
            //Arrange
            const int year = 2009;
            var holidays = new List<Holiday>();
            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 5, 9);
            var calculator = GetCalculator(year);

            //Act
            holidays.Add(HolidayGenerator.CreateHoliday(year: year));
            var sut = calculator.GetBusinessDaysCount(startDate, endDate, holidays);

            //Assert
            sut.Should().BeGreaterOrEqualTo(9);
        }
        
        [Fact]
        public void BusinessDays_GetBusinessDaysCountFromList_Exception()
        {
            //Arrange
            const int year = 2009;
            var startDate = new DateTime(year, 4, 25);
            var endDate = new DateTime(year, 5, 9);
            var calculator = GetCalculator(year);

            //Act
            Action act = () =>
            {
                var sut = calculator.GetBusinessDaysCount(startDate, endDate, null);
            };

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        private BusinessDaysCalculator GetCalculator(int year = 2001, bool hasHolidayFile = false)
        {
            if (hasHolidayFile)
            {
                this.path.FileName = "testFileName";
                this.path.Extension = "test";
                var holidays = new List<Holiday>
                {
                    HolidayGenerator.CreateHoliday(year, id: RandomValuesGenerator.RandomInt(1, 25))
                };
                this.mockFileReadingManager.Setup(x => x.ReadHolidaysFile(It.IsAny<FilePathInfo>())).Returns(holidays);
            }
            else
            {
                this.mockFileReadingManager.Setup(x => x.ReadHolidaysFile(It.IsAny<FilePathInfo>())).Returns(new List<Holiday>());
            }

            var calculator = new BusinessDaysCalculator(this.path, this.mockFileReadingManager.Object);
            return calculator;
        }
    }
}
