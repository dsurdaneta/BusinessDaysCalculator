using DsuDev.BusinessDays.Constants;
using System;

namespace DsuDev.BusinessDays.ConsoleApp1
{
	class Program
    {
        static void Main(string[] args)
        {
            var aux = BusinessDaysCalculator.GetBusinessDaysCount(DateTime.Today, DateTime.Today.AddDays(20), true, fileExt: FileExtension.Csv);
            Console.WriteLine(aux);
            Console.ReadLine();
        }
    }
}