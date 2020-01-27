using System;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Services.FileHandling;
using FluentAssertions;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests.FileHandling
{
    public class JsonReaderTests
    {
        [Fact]
        public void JsonHolidayReader_When_new_Then_hasExpectedProperties()
        {
            // Act
            var reader = new JsonHolidayReader();

            // Assert
            reader.Holidays.Should().NotBeNull();
            reader.Holidays.Count.Should().Be(0);
        }

        [Fact]
        public void Json_GetHolidaysFromFile_When_EmptyPath_Then_ThrowException()
        {
            // Arrange
            var reader = new JsonHolidayReader();
            // Act
            Action action = () => reader.GetHolidaysFromFile(string.Empty);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void Json_GetHolidaysFromFile_When_WrongFileExtension_Then_ThrowException()
        {
            // Arrange
            var reader = new JsonHolidayReader();
            var path = RandomValuesGenerator.RandomString(6);

            // Act
            Action action = () => reader.GetHolidaysFromFile(path);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
