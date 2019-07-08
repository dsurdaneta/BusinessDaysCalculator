using DsuDev.BusinessDays.Tools.FluentBuilders;
using FluentAssertions;
using System;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class DirectoryHelperTests
    {
        [Fact]
        public void ValidateFilePathInfo_When_PathInfoIsNull_ThenThrowException()
        {
            // Act
            Action action = () => DirectoryHelper.ValidateFilePathInfo(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null, "hello")]
        [InlineData("hello", "")]
        public void ValidateFilePathInfo_NotValidPath(string filename, string ext)
        {
            // Arrange
            var pathInfo = new FilePathInfoBuilder().Create()
                .WithFileName(filename)
                .WithExtension(ext)
                .Build();

            // Act
            Action action = () => DirectoryHelper.ValidateFilePathInfo(pathInfo);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
