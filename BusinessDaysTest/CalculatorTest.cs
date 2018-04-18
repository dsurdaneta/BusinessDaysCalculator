using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DsuDev.BusinessDays.Constants;

namespace DsuDev.BusinessDays.Test
{
	[TestClass]
    public class CalculatorTest
    {
		[TestMethod]
		public void BusinessDays_CalculatorObjIsNotNull()
		{
            //Act
			var sut = new BusinessDaysCalculator();
            //Assert
			Assert.IsNotNull(sut);
		}		

		[TestMethod]
		public void BusinessDays_FileExtensionObjIsNotNull()
		{
            //Act
			var sut = new FileExtension();
            //Assert
			Assert.IsNotNull(sut);
		}

        [TestMethod]
        public void BusinessDays_GetBusinessDaysCountNoHolidaysFile()
        {
            //Arrange
            int year = 2001;
            DateTime startDate = new DateTime(year,5,26);
            DateTime expectedDate = new DateTime(year,6,11);
			//Act
            var sut = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate);
			//Assert
            Assert.AreEqual(10, sut);
        }

        [TestMethod]
        public void BusinessDays_AddBusinessDaysNoHolidaysFile()
        {
            //Arrange
            int year = 2001;
            DateTime startDate = new DateTime(year, 5, 26);
			DateTime expectedDate = new DateTime(year,6,15);
			//Act
            var sut = BusinessDaysCalculator.AddBusinessDays(startDate, 15);
            //Assert
            Assert.AreEqual(expectedDate, sut);
        }

        [TestMethod]
        public void BusinessDays_AddBusinessDaysWithHolidaysCounter()
		{
			//Arrange
			DateTime starDate = new DateTime(2001, 5, 26);
			DateTime expectedDate = new DateTime(2001, 6, 18);
			//Act
			var sut = BusinessDaysCalculator.AddBusinessDays(starDate, 15, 3);
			//Assert
			Assert.AreEqual(expectedDate, sut);
		}

		[TestMethod]
		public void BusinessDays_AddBusinessDaysFromList()
		{
            //Arrange
            int year = 2001;
            List<Holiday> holidays = new List<Holiday>();			
			DateTime starDate = new DateTime(year, 4, 27);
			DateTime expectedDate = new DateTime(year, 5, 17);
			//Act
			holidays.Add(HolidayTests.GetWorkersDay(year));
			var sut = BusinessDaysCalculator.AddBusinessDays(starDate, 15, holidays);
			//Assert
			Assert.AreEqual(expectedDate, sut);
		}

		[TestMethod]
		public void BusinessDays_GetBusinessDaysCountFromList()
		{
            //Arrange
            int year = 2001;
			List<Holiday> holidays = new List<Holiday>();
			DateTime startDate = new DateTime(year, 4, 25);
			DateTime expectedDate = new DateTime(year, 5, 9);
			//Act
			holidays.Add(HolidayTests.GetWorkersDay(year));
			var sut = BusinessDaysCalculator.GetBusinessDaysCount(startDate, expectedDate, holidays);
			//Assert
			Assert.AreEqual(9, sut);
		}
		
	}
}
