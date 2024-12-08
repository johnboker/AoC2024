namespace AoC2024.Solutions
{
    public class Day02 : Day
    {
        private List<List<int>> Input1 { get; set; }

        public Day02(string day, bool test) : base(day, test)
        {
            Input1 = Input
                   .Select(a => a.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(b => int.Parse(b)).ToList())
                   .ToList();
        }

        override public void SolvePart1()
        {
            int good = 0;
            foreach (var line in Input1)
            {
                good += IsGood1(line) ? 1 : 0;
            }

            Console.WriteLine($"Day {DayNumber} Part 1: {good}");
        }

        override public void SolvePart2()
        {
            int good = 0;
            foreach (var line in Input1)
            {
                good += IsGood2(line, 0) ? 1 : 0;
            }

            Console.WriteLine($"Day {DayNumber} Part 2: {good}");
        }

        private static bool IsGood1(List<int> line)
        {
            var diff = line[0] - line[line.Count - 1];
            if (diff == 0) return false;

            var increasing = diff < 0 ? -1 : 1;

            for (var i = 0; i < line.Count - 1; i++)
            {
                var d = increasing * (line[i] - line[i + 1]);
                if (d > 3 || d < 1) return false;
            }
            return true;
        }

        private static bool IsGood2(List<int> line, int chance)
        {
            var diff = line[0] - line[^1];
            if (diff == 0) return false;

            var increasing = diff < 0 ? -1 : 1;

            for (var i = 0; i < line.Count - 1; i++)
            {
                var d = increasing * (line[i] - line[i + 1]);
                if ((d > 3 || d < 1) && chance == 1)
                {
                    return false;
                }
                else if (d > 3 || d < 1)
                {
                    var line1 = new List<int>(line);
                    var line2 = new List<int>(line);
                    line1.RemoveAt(i);
                    line2.RemoveAt(i + 1);
                    return IsGood2(line1, 1) || IsGood2(line2, 1);
                }
            }
            return true;
        }
    }
}