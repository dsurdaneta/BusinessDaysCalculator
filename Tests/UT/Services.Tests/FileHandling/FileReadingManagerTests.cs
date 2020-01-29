using System;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.Services.FileHandling;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using FluentAssertions;
using Moq;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests.FileHandling
{
    public class FileReadingManagerTests
    {
        private readonly Mock<IJsonReader> jsonMock;
        private readonly Mock<IXmlReader> xmlMock;
        private readonly Mock<ICsvHolidayReader> csvMock;
        private readonly Mock<ICustomTxtReader> txtMock;

        public FileReadingManagerTests()
        {
            jsonMock = new Mock<IJsonReader>();
            xmlMock = new Mock<IXmlReader>();
            csvMock = new Mock<ICsvHolidayReader>();
            txtMock = new Mock<ICustomTxtReader>();
        }

        private FileReadingManager Setup(int expectedAmount)
        {
            const int year = 2010;
            jsonMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateRandomHolidays(expectedAmount, year));

            xmlMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateRandomHolidays(expectedAmount, year));

            csvMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateRandomHolidays(expectedAmount, year));

            txtMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateRandomHolidays(expectedAmount, year));


            var fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            return fileReading;
        }

        [Theory]
        [ClassData(typeof(FileReadingManagerTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(
            IJsonReader jsonReader, 
            IXmlReader xmlReader, 
            ICsvHolidayReader csvHolidayReader, 
            ICustomTxtReader customReader)
        {
            // Act
            Action action = () => new FileReadingManager(jsonReader, xmlReader, csvHolidayReader, customReader);

            // Assert
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
            IFileReadingManager fileReading = Setup(expectedAmount);
            var pathInfo = FilePathGenerator.CreateBasePath(extension);

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
            IFileReadingManager fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            var ext = "unknown";
            var pathInfo = FilePathGenerator.CreateBasePath(ext);

            // Act
            Action action = () => fileReading.ReadHolidaysFile(pathInfo);

            // Act
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
