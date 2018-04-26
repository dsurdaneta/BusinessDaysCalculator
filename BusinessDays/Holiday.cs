﻿using System;

namespace DsuDev.BusinessDays
{
	/// <summary>
	/// DTO class to handle Holidays info
	/// </summary>
	public class Holiday
	{
		public DateTime HolidayDate { get; set; }
		//in case you need to handle the date as a string:
		public string HolidayStringDate { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Holiday(bool currentYear = false)
		{
			if (currentYear)
				InitializeHolidayDate(DateTime.Today.Year, 1, 1);
			else
				HolidayDate = new DateTime();
		}

		public Holiday(int year, int month, int day) => InitializeHolidayDate(year, month, day);

		public Holiday(DateTime dateTime) => HolidayDate = dateTime;

		public Holiday(TimeSpan timeSpan) => HolidayDate = new DateTime(timeSpan.Ticks);

		private void InitializeHolidayDate(int year, int month, int day)
		{
			HolidayDate = new DateTime(year, month, day);
		}
	}
	
}
