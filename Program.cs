using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2024
{
    class Program
    {
        static void Main(string[] args)
        {
            bool testInput = args.Contains("-t");

            var day = DateTime.Now.Day;

            if (args.Contains("-d"))
            {
                var argIdx = Array.IndexOf(args, "-d") + 1;
                day = Convert.ToInt32(args[argIdx]);
            }

            var dayString = $"{day:00}";
            var t = Type.GetType($"AoC2024.Solutions.Day{dayString}");
            if (t != null)
            {
                if (Activator.CreateInstance(t, [dayString, testInput]) is Day daySolution)
                {
                    Timer.Time(daySolution.SolvePart1);
                    Timer.Time(daySolution.SolvePart2);
                }
            }
        }
    }
}