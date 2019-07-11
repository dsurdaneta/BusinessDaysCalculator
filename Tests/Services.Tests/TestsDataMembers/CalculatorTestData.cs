﻿using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileReaders;
using Moq;
using System.Collections;
using System.Collections.Generic;

namespace DsuDev.BusinessDays.Services.Tests.TestsDataMembers
{
    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var mockFileReadingManager = new Mock<IFileReadingManager>();
            var path = new FilePathInfo();
            yield return new object[] { null, mockFileReadingManager.Object };
            yield return new object[] { path, null };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
