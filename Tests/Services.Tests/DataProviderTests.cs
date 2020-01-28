using AutoMapper;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.Services.Tests.TestHelpers;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services.Tests
{
    public class DataProviderTests
    {
        private Mock<IRepository<DbModels.Holiday>> mockHolidayRepository;
        private Mock<IMapper> mockMapper;

        public DataProviderTests()
        {
            this.mockMapper = new Mock<IMapper>();
            this.mockHolidayRepository = new Mock<IRepository<DbModels.Holiday>>();
        }

        public DataProvider SetuProvider(ICollection<DomainEntities.Holiday> domainHolidays, ICollection<DbModels.Holiday> dbHolidays)
        {
            this.mockMapper = new Mock<IMapper>();
            this.mockHolidayRepository = new Mock<IRepository<DbModels.Holiday>>();

            // setup mapping
            this.mockMapper
                .Setup(setup => setup.Map<ICollection<DomainEntities.Holiday>>(It.IsAny<ICollection<DbModels.Holiday>>()))
                .Returns(domainHolidays);

            this.mockMapper
                .Setup(setup => setup.Map<DomainEntities.Holiday>(It.IsAny<DbModels.Holiday>()))
                .Returns(domainHolidays.FirstOrDefault());

            // setup repository methods
            this.mockHolidayRepository.Setup(setup => setup.GetAllAsync()).Returns(Task.FromResult(dbHolidays));
            this.mockHolidayRepository.Setup(setup => setup.Get(It.IsAny<Func<DbModels.Holiday, bool>> ()))
                .Returns(dbHolidays);

            return new DataProvider(this.mockHolidayRepository.Object, this.mockMapper.Object);
        }

        [Theory]
        [ClassData(typeof(DataProviderTestData))]
        public static void Constructor_When_ParameterIsNull_Then_ThrowsException(
            IMapper mapper,
            IRepository<DbModels.Holiday> holidayRepository)
        {
            // Act
            Action action = () => new DataProvider(holidayRepository, mapper);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetHolidaysByYear_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int baseYear = 2002;
            const int initialAmount = 5;
            
            var domainHolidays = HolidayGenerator.CreateHolidays(initialAmount, baseYear);
            var dbHolidays = HolidayGeneratorToolExtension.CreateDbHolidays(initialAmount, baseYear);
            var otherHoliday = HolidayGeneratorToolExtension.CreateDbHoliday(
                                RandomValuesGenerator.RandomInt(6, 25),
                                year: baseYear + 2);
            dbHolidays.Add(otherHoliday);

            var provider = SetuProvider(domainHolidays, dbHolidays);

            // Act
            var sut = provider.GetHolidays(baseYear);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().BeLessOrEqualTo(dbHolidays.Count);
        }

        [Fact]
        public void GetHolidaysBetweenDates_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int year = 1990;
            
            var startDate = new DateTime(year, 4, 26);
            var endDateTime = new DateTime(year, 5, 11);

            var provider = SetuProvider(
                new List<DomainEntities.Holiday>
                {
                    HolidayGenerator.CreateHoliday(year)
                },
                new List<DbModels.Holiday>
                {
                    HolidayGeneratorToolExtension.CreateDbHoliday(RandomValuesGenerator.RandomInt(6), year)
                });

            // Act
            var sut = provider.GetHolidays(startDate, endDateTime);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(1);
        }
        
        [Fact]
        public async void GetAllHolidaysAsync_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int baseYear = 2002;
            const int initialAmount = 5;
            
            var domainHolidays = HolidayGenerator.CreateHolidays(initialAmount, baseYear);
            var dbHolidays = HolidayGeneratorToolExtension.CreateDbHolidays(initialAmount, baseYear);
            var expectedFirstHoliday = domainHolidays.FirstOrDefault();
            var provider = SetuProvider(domainHolidays, dbHolidays);

            // Act
            var sut = await provider.GetAllHolidaysAsync().ConfigureAwait(false);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(dbHolidays.Count);
            sut.Any(x => x.Name.Equals(expectedFirstHoliday.Name)).Should().BeTrue();
            sut.Any(x => x.HolidayDate.Equals(expectedFirstHoliday.HolidayDate)).Should().BeTrue();
        }
    }
}
