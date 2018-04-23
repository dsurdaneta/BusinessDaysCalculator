using System.Collections.Generic;

namespace DsuDev.BusinessDays
{
	/// <summary>
	/// DTO list class to handle a List of Holidays. 
	/// Just a Container
	/// </summary>
	public class HolidaysInfoList
	{
		public List<Holiday> Holidays { get; set; }

		public HolidaysInfoList() => Holidays = new List<Holiday>();
	}
}
