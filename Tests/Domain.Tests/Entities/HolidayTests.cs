using System;
using DsuDev.BusinessDays.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DsuDev.BusinessDays.Domain.OldTests.Entities
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
        public void Holiday_WithDateTimeConstructor()
        {
            //Act
            var sut = new Holiday(DateTime.Today);
            //Assert
            Assert.IsNull(sut.Name);
            Assert.IsNull(sut.Description);
            Assert.IsNull(sut.HolidayStringDate);
            Assert.IsTrue(sut.HolidayDate.Year.Equals(DateTime.Today.Year));
        }

        [TestMethod]
        public void Holiday_WithTimeSpanConstructor()
        {
            //Act
            var time = DateTime.Today.TimeOfDay;
            var sut = new Holiday(time);
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
    }
}
