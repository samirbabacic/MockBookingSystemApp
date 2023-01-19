using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.Core
{
    public static class RandomGenerator
    {
        private static Random Random = new();
        private static string AlphaNumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


        public static int GenerateInt(int from, int to)
        {
            return Random.Next(from, to);
        }

        public static string GenerateAlpahnumericString(int length)
        {
            return new string(Enumerable.Repeat(AlphaNumericChars, length)
                      .Select(s => s[Random.Next(s.Length)])
                      .ToArray());
        }
    }
}
