using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using DsuDev.BusinessDays.Tools;
using DsuDev.BusinessDays.Tools.Constants;
using DsuDev.BusinessDays.Tools.SampleGenerators;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests.FileReaders
{
    public class FileReadingManagerTests
    {
        private readonly Mock<IJsonReader> jsonMock;
        private readonly Mock<IXmlReader> xmlMock;
        private readonly Mock<ICsvReader> csvMock;
        private readonly Mock<ICustomTxtReader> txtMock;

        public FileReadingManagerTests()
        {
            jsonMock = new Mock<IJsonReader>();
            xmlMock = new Mock<IXmlReader>();
            csvMock = new Mock<ICsvReader>();
            txtMock = new Mock<ICustomTxtReader>();
        }
        private FileReadingManager Setup(int expectedAmount)
        {
            const int year = 2010;
            jsonMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateHolidays(expectedAmount, year));

            xmlMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateHolidays(expectedAmount, year));

            csvMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateHolidays(expectedAmount, year));

            txtMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateHolidays(expectedAmount, year));


            var fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            return fileReading;
        }

        [Theory]
        [ClassData(typeof(FileReadingManagerTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(
            IJsonReader jsonReader, 
            IXmlReader xmlReader, 
            ICsvReader csvReader, 
            ICustomTxtReader customReader)
        {
            // Act
            Action action = () => new FileReadingManager(jsonReader, xmlReader, csvReader, customReader);

            //Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(FileExtension.Json)]
        [InlineData(FileExtension.Xml)]
        [InlineData(FileExtension.Csv)]
        [InlineData(FileExtension.Txt)]
        public void ReadHolidaysFile_When_ValidFileExtension_ReturnsHolidayList(string extension)
        {
            // Arrange
            var expectedAmount = 4;
            var fileReading = Setup(expectedAmount);
            var pathInfo = FilePathGenerator.CreatePath(extension);

            // Act
            var sut = fileReading.ReadHolidaysFile(pathInfo);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(expectedAmount);

            // CleanUp
            DirectoryHelper.RemoveFolder(pathInfo,true);
        }

        [Fact]
        public void ReadHolidaysFile_When_InvalidFileExtension_ThrowsException()
        {
            // Arrange
            var fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            var ext = RandomValuesGenerator.RandomString(3);
            var pathInfo = FilePathGenerator.CreatePath(ext);

            // Act
            Action action = () => fileReading.ReadHolidaysFile(pathInfo);

            // Act
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
