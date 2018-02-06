using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DsuDev.BusinessDays.Test
{
	[TestClass]
    public class CalculatorTest
    {
		[TestMethod]
		public void BussinesDays_CalculatorObjIsNotNull()
		{
			var result = new BusinessDaysCalculator();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void BussinesDays_HolidayObjIsNotNull()
		{
			var result = new Holiday();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void BussinesDays_HolidayInfoListObjIsNotNull()
		{
			var result = new HolidaysInfoList();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void BussinesDays_FileExtensionObjIsNotNull()
		{
			var result = new FileExtension();
			Assert.IsNotNull(result);
		}

		[TestMethod]
        public void BussinesDays_GetBusinessDaysCountNoHolidaysFile()
        {
			//Arrange
            DateTime startDate = new DateTime(2001,5,26);
            DateTime expectedDate = new DateTime(2001,6,11);
			//Act
            var result = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate);
			//Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void BussinesDays_AddBusinessDaysNoHolidaysFile()
        {
			//Arrange
            DateTime startDate = new DateTime(2001, 5, 26);
			DateTime expectedDate = new DateTime(2001,6,15);
			//Act
            var result = BusinessDaysCalculator.AddBusinessDays(startDate, 15);
            //Assert
            Assert.AreEqual(expectedDate, result);
        }

        [TestMethod]
        public void BussinesDays_AddBusinessDaysWithHolidaysCounter()
		{
			//Arrange
			DateTime starDate = new DateTime(2001, 5, 26);
			DateTime expectedDate = new DateTime(2001, 6, 18);
			//Act
			var result = BusinessDaysCalculator.AddBusinessDays(starDate, 15, 3);
			//Assert
			Assert.AreEqual(expectedDate, result);
		}

		[TestMethod]
		public void BussinesDays_AddBusinessDaysFromList()
		{
			//Arrange
			List<Holiday> holidays = new List<Holiday>();			
			DateTime starDate = new DateTime(2001, 4, 27);
			DateTime expectedDate = new DateTime(2001, 5, 17);
			//Act
			holidays.Add(new Holiday
			{
				HolidayDate = new DateTime(2001, 5, 1),
				Name = "Workers Day",
				Description = " "
			});
			var result = BusinessDaysCalculator.AddBusinessDays(starDate, 15, holidays);
			//Assert
			Assert.AreEqual(expectedDate, result);
		}

		[TestMethod]
		public void BussinesDays_GetBusinessDaysCountFromList()
		{
			//Arrange
			List<Holiday> holidays = new List<Holiday>();
			DateTime startDate = new DateTime(2001, 4, 25);
			DateTime expectedDate = new DateTime(2001, 5, 9);
			//Act
			holidays.Add(new Holiday
			{
				HolidayDate = new DateTime(2001, 5, 1),
				Name = "Workers Day",
				Description = " "
			});
			var result = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate, holidays);
			//Assert
			Assert.AreEqual(9, result);
		}
		
	}
}
