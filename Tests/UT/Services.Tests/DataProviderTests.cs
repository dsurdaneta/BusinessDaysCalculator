﻿using AutoMapper;
using DsuDev.BusinessDays.Common.Tools;
using DsuDev.BusinessDays.Common.Tools.SampleGenerators;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.Services.Interfaces;
using DsuDev.BusinessDays.Tests.Helper.SampleGenerators;
using DsuDev.BusinessDays.Tests.Helper.TestsDataMembers;
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
        public void GetHolidaysByYear_When_SameYear_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int baseYear = 2002;
            const int initialAmount = 5;
            
            var domainHolidays = HolidayGenerator.CreateRandomHolidays(initialAmount, baseYear);
            var dbHolidays = DbHolidayGenerator.CreateRandomDbHolidays(initialAmount, baseYear);
            var otherHoliday = DbHolidayGenerator.CreateDbHoliday(
                                RandomValuesGenerator.RandomInt(6, 25),
                                year: baseYear + 2);
            dbHolidays.Add(otherHoliday);

            IDataProvider provider = SetuProvider(domainHolidays, dbHolidays);

            // Act
            var sut = provider.GetHolidays(baseYear);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().BeLessOrEqualTo(dbHolidays.Count);
        }
        
        [Fact]
        public void GetHolidaysByYear_When_OtherYear_Then_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int baseYear = 2002;
            const int initialAmount = 5;
            
            var dbHolidays = DbHolidayGenerator.CreateRandomDbHolidays(initialAmount, baseYear +2);
            IDataProvider provider = SetuProvider(new List<DomainEntities.Holiday>(), dbHolidays);

            // Act
            var sut = provider.GetHolidays(baseYear - 1);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().BeLessOrEqualTo(dbHolidays.Count);
        }

        [Fact]
        public void GetHolidaysBetweenDates_When_ValidDates_Then_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int year = 1990;
            
            var startDate = new DateTime(year, 4, 26);
            var endDateTime = new DateTime(year, 5, 11);

            IDataProvider provider = SetuProvider(
                new List<DomainEntities.Holiday>
                {
                    HolidayGenerator.CreateHoliday(year)
                },
                new List<DbModels.Holiday>
                {
                    DbHolidayGenerator.CreateDbHoliday(RandomValuesGenerator.RandomInt(6), year)
                });

            // Act
            var sut = provider.GetHolidays(startDate, endDateTime);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(1);
        }
        
        [Fact]
        public void GetHolidaysBetweenDates_When_OtherDates_Then_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int year = 1990;
            
            var startDate = new DateTime(year, 5, 26);
            var endDateTime = new DateTime(year, 6, 11);

            IDataProvider provider = SetuProvider(
                new List<DomainEntities.Holiday>(),
                new List<DbModels.Holiday>
                {
                    DbHolidayGenerator.CreateDbHoliday(RandomValuesGenerator.RandomInt(6), year)
                });

            // Act
            var sut = provider.GetHolidays(startDate, endDateTime);

            // Assert
            sut.Should().NotBeNull();
            sut.Count.Should().Be(0);
        }
        
        [Fact]
        public async void GetAllHolidaysAsync_GetsCorrectlyTheHolidays()
        {
            // Arrange
            const int baseYear = 2002;
            const int initialAmount = 5;
            
            var domainHolidays = HolidayGenerator.CreateRandomHolidays(initialAmount, baseYear);
            var dbHolidays = DbHolidayGenerator.CreateRandomDbHolidays(initialAmount, baseYear);
            var expectedFirstHoliday = domainHolidays.FirstOrDefault();

            IDataProvider provider = SetuProvider(domainHolidays, dbHolidays);

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
