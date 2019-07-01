using System;
using System.Linq;
using System.Text;

namespace DsuDev.BusinessDays.Tools
{
    public static class RandomValuesGenerator
    {
        const string Chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                builder.Append(Enumerable.Range(0, size)
                    .Select(x => Chars[random.Next(1, Chars.Length)]));
            }

            return builder.ToString();
        }

        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return min > 0 ? random.Next(min, max) : random.Next(max);
        }
    }
}
