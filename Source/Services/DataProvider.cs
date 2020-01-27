using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.Services.Interfaces;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;
using DomainEntities = DsuDev.BusinessDays.Domain.Entities;

namespace DsuDev.BusinessDays.Services
{
    public class DataProvider : IDataProvider
    {
        private readonly IRepository<DbModels.Holiday> holidayRepository;
        private readonly IMapper mapper;

        public DataProvider(IRepository<DbModels.Holiday> repository, IMapper mapper)
        {
            this.holidayRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<ICollection<DomainEntities.Holiday>> GetAllHolidaysAsync()
        {
            var dbHolidays = await this.holidayRepository.GetAllAsync().ConfigureAwait(false);
            var domainHolidays = this.mapper.Map<ICollection<DomainEntities.Holiday>>(dbHolidays);
            return domainHolidays;
        }

        /// <inheritdoc />
        public ICollection<DomainEntities.Holiday> GetHolidays(int year)
        {
            var dbHolidays = this.holidayRepository.Get(h => h.Year == year);
            var domainHolidays = this.mapper.Map<ICollection<DomainEntities.Holiday>>(dbHolidays);
            return domainHolidays;
        }

        /// <inheritdoc />
        public ICollection<DomainEntities.Holiday> GetHolidays(DateTime startDateTime, DateTime endDateTime)
        {
            bool HolidayIsBetweenDates(DbModels.Holiday holiday) => holiday.HolidayDate.ToUniversalTime() >= startDateTime.ToUniversalTime()
                                                           && holiday.HolidayDate.ToUniversalTime() <= endDateTime.ToUniversalTime();

            var dbHolidays = this.holidayRepository.Get(HolidayIsBetweenDates);
            var domainHolidays = this.mapper.Map<ICollection<DomainEntities.Holiday>>(dbHolidays);
            return domainHolidays;
        }
    }
}
