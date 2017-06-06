using System;
using System.Collections.Generic;
using DsuDev.BusinessDays;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var aux = BusinessDaysCalculator.GetBusinessDaysCount(DateTime.Today, DateTime.Today.AddDays(20),true);
            Console.ReadLine();
        }
    }
}