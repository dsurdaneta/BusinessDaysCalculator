using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Moq;

namespace DsuDev.BusinessDays.Services.Tests.TestsDataMembers
{
    public class FileLoaderTestData : IEnumerable<object[]>
    {
        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            var mockMapper = new Mock<IMapper>();
            var mockFileReadingManager = new Mock<IFileReadingManager>();

            yield return new object[] { null, mockFileReadingManager.Object};
            yield return new object[] { mockMapper.Object, null };
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
