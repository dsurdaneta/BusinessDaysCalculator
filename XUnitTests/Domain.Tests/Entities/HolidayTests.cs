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
    }
}
