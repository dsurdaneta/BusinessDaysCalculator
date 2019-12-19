using DsuDev.BusinessDays.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DsuDev.BusinessDays.Domain.Tests.Entities
{
    public class HolidayTests
    {
        [Fact]
        public void Holiday_HolidayObjIsNotNull()
        {
            //Act
            var sut = new Holiday();

            //Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public void Holiday_WithVoidConstructorStringsAreNull()
        {
            //Act
            var sut = new Holiday();

            //Assert
            sut.Should().NotBeNull();
            sut.Name.Should().BeNull();
            sut.Description.Should().BeNull();
            sut.HolidayStringDate.Should().BeNull();
        }

        [Fact]
        public void Holiday_WithDateTimeConstructor()
        {
            //Act
            var sut = new Holiday(DateTime.Today);

            //Assert
            sut.Should().NotBeNull();
            sut.Name.Should().BeNull();
            sut.Description.Should().BeNull();
            sut.Description.Should().BeNull();
            sut.HolidayDate.Year.Should().Be(DateTime.Today.Year);
            sut.HolidayDate.Month.Should().Be(DateTime.Today.Month);
            sut.HolidayDate.Day.Should().Be(DateTime.Today.Day);
        }

        [Fact]
        public void Holiday_WithTimeSpanConstructor()
        {
            //Arrange
            var time = DateTime.Today.TimeOfDay;

            //Act
            var sut = new Holiday(time);
            
            //Assert
            sut.Should().NotBeNull();
            sut.Name.Should().BeNull();
            sut.Description.Should().BeNull();
            sut.HolidayStringDate.Should().BeNull();
        }
        
        [Fact]
        public void Holiday_ConstructorCurrentYear()
        {
            //Act
            var sut = new Holiday(currentYear: true);

            //Assert
            sut.Should().NotBeNull();
            sut.HolidayDate.Year.Should().Be(DateTime.Today.Year);
        }

        [Fact]
        public void Holiday_WithIntDateConstructorGeneratesCorrectDate()
        {
            //Arrange 
            var expectedYear = 2001;
            var expectedMonth = 7;
            var expectedDay = 19;
            
            //Act
            var sut = new Holiday(expectedYear, expectedMonth, expectedDay);
            
            //Assert
            sut.Should().NotBeNull();
            sut.HolidayDate.Year.Should().Be(expectedYear);
            sut.HolidayDate.Month.Should().Be(expectedMonth);
            sut.HolidayDate.Day.Should().Be(expectedDay);
        }

        [Fact]
        public void HolidayEquality_NotEquals()
        {
            // Arrange
            var someHoliday = new Holiday(2003,7,6) { Name = "Some"};
            var otherHoliday = new Holiday(2015,11,28) { Name = "Other"};

            // Act
            var sut = someHoliday.Equals(otherHoliday);

            // Assert
            sut.Should().BeFalse();
        }
        
        [Fact]
        public void HolidayEquality_Equals()
        {
            // Arrange
            var someHoliday = new Holiday(2003,10,8) { Name = "Holiday"};
            var otherHoliday = new Holiday(2015,10,8) { Name = "Holiday"};

            // Act
            var sut = someHoliday.Equals(otherHoliday);

            // Assert
            sut.Should().BeTrue();
        }
        
        [Fact]
        public void HolidayEquality_NullHoliday()
        {
            // Arrange
            var someHoliday = new Holiday(2013,2,26) { Name = "Hello"};

            // Act
            var sut = someHoliday.Equals(null);

            // Assert
            sut.Should().BeFalse();
        }
    }
}
