using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DsuDev.BusinessDays;

namespace BusinessDays.Test
{
    [TestClass]
    public class Calculator
    {
        [TestMethod]
        public void GetBusinessDaysCountNoHolidays()
        {
            DateTime starDate = new DateTime(2001,5,26);
            DateTime enDateTime = new DateTime(2001,6,11);
            var result = BusinessDaysCalculator.GetBusinessDaysCount(starDate, enDateTime);
            Assert.AreEqual(10, result);
        }
    }
}
