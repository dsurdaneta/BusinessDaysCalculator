using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.DataAccess;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces;

namespace DsuDev.BusinessDays.Services
{
    public class DataProvider : IDataProvider
    {
        private readonly IRepository<Holiday> holidayRepository;

        public DataProvider(IRepository<Holiday> repository)
        {
            this.holidayRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc />
        public async Task<ICollection<Holiday>> GetAllHolidaysAsync()
        {
            return await this.holidayRepository.GetAllAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public ICollection<Holiday> GetHolidays(int year)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ICollection<Holiday> GetHolidays(DateTime startDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }
    }
}
