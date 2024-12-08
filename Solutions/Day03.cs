using System.Text.RegularExpressions;

namespace AoC2024.Solutions
{
    public class Day03 : Day
    {
        public Day03(string day, bool test) : base(day, test) { }

        override public void SolvePart1()
        {
            var input = $"do(){string.Join("", Input)}";
            int sum = 0;

            var matches = Regex.Matches(input, @"mul\((?<A>\d+),(?<B>\d+)\)")
                .Cast<Match>()
                .Select(x => (int.Parse(x.Groups["A"].Value), int.Parse(x.Groups["B"].Value)));

            sum += matches.Sum(a => a.Item1 * a.Item2);

            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            var input = $"do(){string.Join("", Input)}don't()";
            int sum = 0;

            var line = string.Join("", Regex.Matches(input, @"do\(\)(?<LINE>.*?)don't\(\)").Cast<Match>()
                .Select(x => x.Groups["LINE"]));

            var muls = Regex.Matches(line, @"mul\((?<A>\d+),(?<B>\d+)\)")
                .Cast<Match>()
                .Select(x => (int.Parse(x.Groups["A"].Value), int.Parse(x.Groups["B"].Value)));

            sum += muls.Sum(a => a.Item1 * a.Item2);
            Console.WriteLine($"Day {DayNumber} Part 2: {sum}");
        }
    }
}