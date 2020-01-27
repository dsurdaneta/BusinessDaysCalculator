using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.Services.Tests.TestsDataMembers;
using FluentAssertions;
using Moq;
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

        // TODO tests for ICollection<DomainEntities.Holiday> GetHolidays(int year)
        // TODO tests for ICollection<DomainEntities.Holiday> GetHolidays(DateTime startDateTime, DateTime endDateTime)
        // TODO tests for async Task<ICollection<DomainEntities.Holiday>> GetAllHolidaysAsync()
    }
}
