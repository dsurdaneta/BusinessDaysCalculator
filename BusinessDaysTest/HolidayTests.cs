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
        public void Holiday_WithVoidConstructorStringsAreNull()
        {
            //Act
            var sut = new Holiday();
            //Assert
            Assert.IsNull(sut.Name);
            Assert.IsNull(sut.Description);
            Assert.IsNull(sut.HolidayStringDate);
        }


        [TestMethod]
        public void Holiday_WithVoidConstructorHolidayDateIsNotNull()
        {
            //Act
            var sut = new Holiday();
            //Assert
            Assert.IsNotNull(sut.HolidayDate);
        }

        [TestMethod]
        public void Holiday_ConstructorCurrentDate()
        {
            //Act
            var sut = new Holiday(currentYear: true);
            //Assert
            Assert.AreEqual(DateTime.Today.Year, sut.HolidayDate.Year);
        }

        [TestMethod]
        public void Holiday_WithIntDateConstructorGeneratesCorrectDate()
        {
            //Arrange 
            var expectedYear = 2001;
            var expectedMonth = 7;
            var expectedDay = 19;
            //Act
            var sut = new Holiday(expectedYear, expectedMonth, expectedDay);
            //Assert
            Assert.AreEqual(sut.HolidayDate.Year, expectedYear);
            Assert.AreEqual(sut.HolidayDate.Month, expectedMonth);
            Assert.AreEqual(sut.HolidayDate.Day, expectedDay);
        }

        [TestMethod]
        public void InfoList_WithVoidConstructorHolidayInfoListObjIsNotNull()
        {
            //Act
            var sut = new HolidaysInfoList();
            //Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void InfoList_WithVoidConstructorHolidayInfoListNotNullHolidaysList()
        {
            //Act
            var sut = new HolidaysInfoList();
            //Assert
            Assert.IsNotNull(sut.Holidays);
        }

        #region Helper Methods
        public static Holiday GenerateHoliday(int year = 2001, string description = " ", string name = "Workers Day")
        {
            return new Holiday
            {
                HolidayDate = new DateTime(year, 5, 1),
                Name = name,
                Description = description
            };
        }
        #endregion
    }
}
