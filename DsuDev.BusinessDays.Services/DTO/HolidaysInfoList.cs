using System.Collections.Generic;
using DsuDev.BusinessDays.Domain.Entities;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Services.DTO
{
    /// <summary>
    /// DTO list class to handle a List of Holidays. 
    /// Just a Container
    /// </summary>
    public class HolidaysInfoList
    {
        public List<Holiday> Holidays { get; set; }

        [JsonConstructor]
        public HolidaysInfoList() => Holidays = new List<Holiday>();
    }
}
