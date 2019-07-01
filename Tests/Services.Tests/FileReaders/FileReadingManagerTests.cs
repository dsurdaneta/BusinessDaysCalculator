using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using DsuDev.BusinessDays.Tools;
using DsuDev.BusinessDays.Tools.FluentBuilders;
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

        [Theory]
        [ClassData(typeof(FileReadingManagerTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(
            IJsonReader jsonReader, 
            IXmlReader xmlReader, 
            ICsvReader csvReader, 
            ICustomTxtReader customReader)
        {
            //Assert
            Assert.Throws<ArgumentNullException>(
                () => new FileReadingManager(jsonReader, xmlReader, csvReader, customReader));
        }

        [Fact]
        public void ReadHolidaysFile_When_ValidFileExtension_ReturnsHolidayList()
        {
            // Arrange
            var expectedAmount = 4;
            jsonMock.Setup(setup => setup.GetHolidaysFromFile(It.IsAny<string>()))
                .Returns(HolidayGenerator.CreateHolidays(expectedAmount, 2010));
            
            var fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            var pathInfo = new FilePathInfoBuilder().Create()
                .WithFolder(Resources.ContainingFolderName)
                .WithFileName(Resources.FileName)
                .WithExtension(FileExtension.Json)
                .Build();

            // Act
            var sut = fileReading.ReadHolidaysFile(pathInfo);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(expectedAmount);
        }
        
        [Fact]
        public void ReadHolidaysFile_When_InvalidFileExtension_ThrowsException()
        {
            // Arrange
            var fileReading = new FileReadingManager(jsonMock.Object, xmlMock.Object, csvMock.Object, txtMock.Object);
            var ext = RandomValuesGenerator.RandomString(3);
            var pathInfo = new FilePathInfoBuilder().Create()
                .WithFolder(Resources.ContainingFolderName)
                .WithFileName(Resources.FileName)
                .WithExtension(ext)
                .Build();

            // Act
            Action action = () => fileReading.ReadHolidaysFile(pathInfo);

            // Act
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
