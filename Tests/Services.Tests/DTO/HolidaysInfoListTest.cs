using DsuDev.BusinessDays.Services.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DsuDev.BusinessDays.Services.Tests.DTO
{
    [TestClass]
    public class HolidaysInfoListTest
    {
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
    }
}
