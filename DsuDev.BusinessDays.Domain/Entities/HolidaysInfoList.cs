using System.Collections.Generic;
using Newtonsoft.Json;

namespace DsuDev.BusinessDays.Domain.Entities
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
