using DsuDev.BusinessDays.Services.DTO;
using FluentAssertions;
using Xunit;

namespace DsuDev.BusinessDays.Services.Tests.DTO
{
    public class HolidaysInfoListTest
    {
        [Fact]
        public void InfoList_WithVoidConstructorHolidayInfoListObjIsNotNull()
        {
            //Act
            var sut = new HolidaysInfoList();
            
            //Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public void InfoList_WithVoidConstructorHolidayInfoListNotNullHolidaysList()
        {
            //Act
            var sut = new HolidaysInfoList();
            
            //Assert
            sut.Should().NotBeNull();
            sut.Holidays.Should().NotBeNull();
            sut.Holidays.Count.Should().Be(0);
        }
    }
}
