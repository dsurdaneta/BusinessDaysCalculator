using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DsuDev.BusinessDays.Common.Tools
{
    /// <summary>
    /// Class to help the creation of random values
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RandomValuesGenerator
    {
        private const string ValidChars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomString(int size)
        {
            Random random = new Random();
            var chars = Enumerable.Range(1, size)
                .Select(x => ValidChars[random.Next(1, ValidChars.Length)]);
            return new string(chars.ToArray());
        }

        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return min > 0 ? random.Next(min, max) : random.Next(max);
        }
    }
}
