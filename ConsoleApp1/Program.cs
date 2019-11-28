using DsuDev.BusinessDays.Services;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DsuDev.BusinessDays.ConsoleApp1
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        //TODO: implement tests to validate holidays file reading by mocking the IFileReadingManager and all the readers
        private static void Main(string[] args)
        {
            //var aux = BusinessDaysCalculator.GetBusinessDaysCount(DateTime.Today, DateTime.Today.AddDays(20), true, fileExt: FileExtension.Json);
            //var aux = BusinessDaysCalculator.GetBusinessDaysCount(DateTime.Today, new DateTime(2018,5,21), true, fileExt: FileExtension.Json);
            //Console.WriteLine(aux);
            Console.ReadLine();
        }
    }
}