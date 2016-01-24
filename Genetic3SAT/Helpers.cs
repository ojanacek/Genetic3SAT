using System;
using System.Collections;

namespace Genetic3SAT
{
    public static class Helpers
    {
        public static string PadLeft<T>(this T item, int totalWidth)
        {
            return item.ToString().PadLeft(totalWidth, ' ');
        }

        public static bool ToVariableValue(this int number, BitArray gens)
        {
            return number < 0 ? !gens[Math.Abs(number) - 1] : gens[number - 1];
        }
    }
}