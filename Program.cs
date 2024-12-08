using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using HandlebarsDotNet;

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
            else
            {
                GenerateFiles(dayString);
            }
        }

        private static void GenerateFiles(string dayString)
        {
            var template = Handlebars.Compile(File.ReadAllText("Templates/Day.hbs"));
            using var writer =  File.CreateText($"Solutions/Day{dayString}.cs");
            writer.Write(template(new { DayString = dayString }));
            using var s1 = File.Create($"Inputs/input{dayString}.txt");
            using var s2 = File.Create($"Inputs/input{dayString}_test.txt");
        }
    }
}