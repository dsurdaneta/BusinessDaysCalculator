using System.Collections;
using System.Collections.Generic;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;
using Moq;

namespace DsuDev.BusinessDays.Tests.Helper.TestsDataMembers.FileHandling
{
    public class FileReadingManagerTestData : IEnumerable<object[]>
    {
        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            var jsonMock = new Mock<IJsonReader>();
            var xmlMock = new Mock<IXmlReader>();
            var csvMock = new Mock<ICsvHolidayReader>();
            var txtMock = new Mock<ICustomTxtReader>();

            yield return new object[] {null, xmlMock.Object, csvMock.Object, txtMock.Object };
            yield return new object[] {jsonMock.Object, null, csvMock.Object, txtMock.Object };
            yield return new object[] {jsonMock.Object, xmlMock.Object, null, txtMock.Object };
            yield return new object[] {jsonMock.Object, xmlMock.Object, csvMock.Object, null };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
