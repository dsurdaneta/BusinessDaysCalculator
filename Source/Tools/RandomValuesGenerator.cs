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

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="size">The size of the desired string.</param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            Random random = new Random();
            var chars = Enumerable.Range(1, size)
                .Select(x => ValidChars[random.Next(1, ValidChars.Length)]);
            return new string(chars.ToArray());
        }

        /// <summary>
        /// Generates a random int
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return min > 0 ? random.Next(min, max) : random.Next(max);
        }
        
        /// <summary>
        /// Generates a random int
        /// </summary>
        /// <param name="max">The maximum value.</param>
        /// <returns></returns>
        public static int RandomInt(int max) => RandomInt(0, max);

        /// <summary>
        /// Generates the random boolean.
        /// </summary>
        /// <returns></returns>
        public static bool RandomBoolean()
        {
            Random random = new Random();
            return random.Next(ValidChars.Length) % 2 == 0;
        }
    }
}
