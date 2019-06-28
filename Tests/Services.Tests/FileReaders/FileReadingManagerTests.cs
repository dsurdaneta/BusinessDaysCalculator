using System;
using System.Collections.Generic;
using System.Text;
using DsuDev.BusinessDays.Services.FileReaders;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using Moq;
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
    }
}
