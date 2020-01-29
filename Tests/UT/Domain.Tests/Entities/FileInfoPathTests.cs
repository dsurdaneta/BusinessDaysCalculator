using DsuDev.BusinessDays.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace DsuDev.BusinessDays.Domain.Tests.Entities
{
    public class FileInfoPathTests
    {
        [Fact]
        public void FileInfoPath_WithVoidContainer()
        {
            // Act
            var sut = new FilePathInfo();

            // Assert
            sut.Should().NotBeNull();
            sut.IsAbsolutePath.Should().BeFalse();
            sut.FileName.Should().BeEmpty();
            sut.Extension.Should().BeEmpty();
            sut.Folder.Should().BeEmpty();
        }
    }
}
