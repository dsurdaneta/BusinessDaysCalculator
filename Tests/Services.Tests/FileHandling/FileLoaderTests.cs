using System;
using System.Collections.Generic;
using AutoMapper;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.Services.FileHandling;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using FluentAssertions;
using Moq;
using Xunit;

using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Tests.FileHandling
{
    public class FileLoaderTests
    {
        private Mock<IMapper> mockMapper;
        private Mock<IFileReadingManager> mockFileReadingManager;
        private DomainEntities.FilePathInfo filePathInfo;

        public FileLoaderTests()
        {
            this.mockMapper = new Mock<IMapper>();
            this.mockFileReadingManager = new Mock<IFileReadingManager>();
            this.filePathInfo = FilePathGenerator.CreateBasePath(RandomValuesGenerator.RandomString(5));
        }

        public FileLoader SetupFileLoader(List<DomainEntities.Holiday> holidays, string basePath, bool throwException = false)
        {
            this.mockMapper = new Mock<IMapper>();
            this.mockFileReadingManager = new Mock<IFileReadingManager>();
            this.filePathInfo = FilePathGenerator.CreateBasePath(basePath);

            if (throwException)
            {
                this.mockFileReadingManager
                    .Setup(setup => setup.ReadHolidaysFile(It.IsAny<DomainEntities.FilePathInfo>()))
                    .Throws(new InvalidOperationException());
            }
            else
            {
                this.mockFileReadingManager.Setup(setup => setup.ReadHolidaysFile(It.IsAny<DomainEntities.FilePathInfo>()))
                    .Returns(holidays);
            }

            return new FileLoader(this.mockMapper.Object, this.mockFileReadingManager.Object, this.filePathInfo);
        }

        [Theory]
        [ClassData(typeof(FileLoaderTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(
            IMapper mapper,
            IFileReadingManager fileReadingManager)
        {
            // Act
            Action action = () => new FileLoader(mapper, fileReadingManager, new DomainEntities.FilePathInfo());

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SaveHolidays_IsNotImplemented()
        {
            // Arrange
            var holidays = HolidayGenerator.CreateRandomHolidays(5, DateTime.Today.Year);
            var basePath = RandomValuesGenerator.RandomString(5);

            IFileLoader loader = this.SetupFileLoader(holidays, basePath, false);

            // Act
            Action action = () => loader.SaveHolidays();

            action.Should().Throw<NotImplementedException>();
        }

        [Fact]
        public void Constructor_When_FilePathInfoParameterIsNull_Then_DefaultValuesAreAssigned()
        {
            // Act
            IFileLoader loader = new FileLoader(this.mockMapper.Object, this. mockFileReadingManager.Object, null);

            // Assert
            loader.Should().NotBeNull();
            loader.Holidays.Should().NotBeNull();
            loader.Holidays.Count.Should().Be(0);
            loader.FilePathInfo.Should().NotBeNull();
            var info = loader.FilePathInfo;
            info.Extension.Should().Be(FileExtension.Json);
            info.FileName.Should().Be(Resources.FileName);
            info.Folder.Should().Be(Resources.ContainingFolderName);
            info.IsAbsolutePath.Should().BeFalse();
        }

        [Fact]
        public void Constructor_When_Default_Then_DefaultValuesAreAssigned()
        {
            // Act
            IFileLoader loader = new FileLoader(this.mockMapper.Object);

            // Assert
            loader.Should().NotBeNull();
            loader.Holidays.Should().NotBeNull();
            loader.Holidays.Count.Should().Be(0);
            loader.FilePathInfo.Should().NotBeNull();
            var info = loader.FilePathInfo;
            info.Extension.Should().Be(FileExtension.Json);
            info.FileName.Should().Be(Resources.FileName);
            info.Folder.Should().Be(Resources.ContainingFolderName);
            info.IsAbsolutePath.Should().BeFalse();
        }

        [Fact]
        public void LoadFile_When_NullParameter_Then_HolidaysAreLoadedFromDB()
        {
            // Arrange
            var holidays = HolidayGenerator.CreateRandomHolidays(5, DateTime.Today.Year);
            var basePath = RandomValuesGenerator.RandomString(5);
            var loader = this.SetupFileLoader(holidays, basePath, false);
            
            // Act
            var result = loader.LoadFile();

            // Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public void LoadFile_When_EmptyFile_Then_NoHolidaysAreLoaded()
        {
            // Arrange
            var holidays = new List<DomainEntities.Holiday>();
            var basePath = RandomValuesGenerator.RandomString(5);

            IFileLoader loader = this.SetupFileLoader(holidays, basePath, false);

            var otherInfo = FilePathGenerator.CreateBasePath(basePath);

            // Act
            var result = loader.LoadFile(otherInfo);

            // Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public void LoadFile_When_Error_Then_ThrowsException()
        {
            // Arrange
            var basePath = RandomValuesGenerator.RandomString(5);

            IFileLoader loader = this.SetupFileLoader(new List<DomainEntities.Holiday>(), basePath, true);
            
            // Act
            Action action = () =>
            {
                var result = loader.LoadFile();
            };

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
        
        [Fact]
        public void LoadFile_When_InvalidPath_Then_ThrowsException()
        {
            // Arrange
            IFileLoader loader = this.SetupFileLoader(new List<DomainEntities.Holiday>(), string.Empty, true);
            
            // Act
            Action action = () =>
            {
                var result = loader.LoadFile();
            };

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
