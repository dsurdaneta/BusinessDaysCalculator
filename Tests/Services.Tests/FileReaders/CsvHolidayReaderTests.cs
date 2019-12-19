using System;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Tools;
using FluentAssertions;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests.FileReaders
{
    public class CsvHolidayReaderTests
    {
        [Theory]
        [InlineData(true, "")]
        [InlineData(false, null)]
        [InlineData(false, ":")]
        [InlineData(true, "|")]
        public void CsvHolidayReader_When_new_Then_hasExpectedProperties(bool hasHeader, string delimiter)
        {
            // Act
            var expectedDelimiter =  string.IsNullOrWhiteSpace(delimiter)  ? ";" : delimiter;
            var csvReader = new CsvHolidayReader(hasHeader, delimiter);

            // Arrange
            csvReader.Should().NotBeNull();
            csvReader.HasHeaderRecord.Should().Be(hasHeader);
            csvReader.Delimiter.Should().Be(expectedDelimiter);
            csvReader.Holidays.Should().NotBeNull();
            csvReader.Holidays.Count.Should().Be(0);
        }

        [Fact]
        public void Csv_GetHolidaysFromFile_When_EmptyPath_Then_ThrowException()
        {
            // Arrange
            var reader = new CsvHolidayReader();
            // Act
            Action action = () => reader.GetHolidaysFromFile(string.Empty);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void Csv_GetHolidaysFromFile_When_WrongFileExtension_Then_ThrowException()
        {
            // Arrange
            var reader = new CsvHolidayReader();
            var path = RandomValuesGenerator.RandomString(6);

            // Act
            Action action = () => reader.GetHolidaysFromFile(path);

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
