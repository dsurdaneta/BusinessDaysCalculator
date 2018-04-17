using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DsuDev.BusinessDays.Test
{
    [TestClass]
    public class HolidayTests
    {
        [TestMethod]
        public void Holiday_HolidayObjIsNotNull()
        {
            //Act
            var sut = new Holiday();
            //Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Holiday_HolidayDateIsNotNull()
        {
            //Act
            var sut = new Holiday();
            //Assert
            Assert.IsNotNull(sut.HolidayDate);
        }

        [TestMethod]
        public void Holiday_HolidayInfoListObjIsNotNull()
        {
            //Act
            var sut = new HolidaysInfoList();
            //Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Holiday_HolidayInfoListNotNullHolidaysList()
        {
            //Act
            var sut = new HolidaysInfoList();
            //Assert
            Assert.IsNotNull(sut.Holidays);
        }

        #region Helper Methods
        public static Holiday GetWorkersDay(int year = 2001, string description = " ")
        {
            return new Holiday
            {
                HolidayDate = new DateTime(year, 5, 1),
                Name = "Workers Day",
                Description = description
            };
        }
        #endregion
    }
}
