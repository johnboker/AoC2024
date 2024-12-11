using System.Linq;
using Microsoft.VisualBasic;

namespace AoC2024.Solutions
{
    public class Day11 : Day
    {
        public Day11(string day, bool test) : base(day, test)
        {

        }

        override public void SolvePart1()
        {
            long sum = 0;
            var input = Input[0].Split(" ").Select(a => long.Parse(a));
            var stones = input.ToDictionary(k => k, v => input.LongCount(a => a == v));
            GetStonesForIterations(stones, 25);
            sum = stones.Sum(a => a.Value);
            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            long sum = 0;
            var input = Input[0].Split(" ").Select(a => long.Parse(a));
            var stones = input.ToDictionary(k => k, v => input.LongCount(a => a == v));
            GetStonesForIterations(stones, 75);
            sum = stones.Sum(a => a.Value);
            Console.WriteLine($"Day {DayNumber} Part 2: {sum}");
        }

        public void AddModification(long key, long value, Dictionary<long, long> modifications)
        {
            if (!modifications.TryAdd(key, value))
            {
                modifications[key] += value;
            }
        }

        public void GetStonesForIterations(Dictionary<long, long> stones, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                var modifications = new Dictionary<long, long>();
                foreach (var stone in stones)
                {
                    if (stone.Key == 0)
                    {
                        AddModification(1, stone.Value, modifications);
                    }
                    else if (stone.Key.ToString().Length % 2 == 0)
                    {
                        var str = stone.Key.ToString();
                        var len = str.Length;

                        var left = long.Parse(str[..(len / 2)]);
                        var right = long.Parse(str[(len / 2)..]);

                        AddModification(left, stone.Value, modifications);
                        AddModification(right, stone.Value, modifications);
                    }
                    else
                    {
                        AddModification(stone.Key * 2024, stone.Value, modifications);
                    }

                    stones.Remove(stone.Key);
                }

                foreach (var m in modifications)
                {
                    stones[m.Key] = m.Value;
                }
            }
        }
    }
}