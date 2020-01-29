using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using DsuDev.BusinessDays.DataAccess;
using Moq;
using DbModels = DsuDev.BusinessDays.DataAccess.Models;

namespace DsuDev.BusinessDays.Tests.Helper.TestsDataMembers
{
    public class DataProviderTestData : IEnumerable<object[]>
    {
        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            var mockMapper = new Mock<IMapper>();
            var mockHolidayRepository = new Mock<IRepository<DbModels.Holiday>>();

            yield return new object[] { null, mockHolidayRepository.Object };
            yield return new object[] { mockMapper.Object, null };
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
