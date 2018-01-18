using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DsuDev.BusinessDays.Test
{
	[TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void BussinesDays_GetBusinessDaysCountNoHolidaysFile()
        {
            DateTime startDate = new DateTime(2001,5,26);
            DateTime expectedDate = new DateTime(2001,6,11);
            var result = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void BussinesDays_AddBusinessDaysNoHolidaysFile()
        {
            DateTime startDate = new DateTime(2001, 5, 26);
            var result = BusinessDaysCalculator.AddBusinessDays(startDate, 15);
            DateTime expectedDate = new DateTime(2001,6,15);
            Assert.AreEqual(expectedDate, result);
        }

        [TestMethod]
        public void BussinesDays_AddBusinessDaysWithHolidaysCounter()
        {
            DateTime starDate = new DateTime(2001, 5, 26);
            var result = BusinessDaysCalculator.AddBusinessDays(starDate, 15,3);
            DateTime expectedDate = new DateTime(2001, 6, 18);
            Assert.AreEqual(expectedDate, result);
        }
    }
}
