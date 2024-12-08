namespace AoC2024.Solutions
{
    public class Day01 : Day
    {
        private List<int> Input1 { get; set; }
        private List<int> Input2 { get; set; }

        public Day01(string day, bool test) : base(day, test)
        {
            var input = Input
                   .Select(a => a.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                   .Select(a => (int.Parse(a[0]), int.Parse(a[1])))
                   .ToList();

            Input1 = input.Select(a => a.Item1).OrderBy(a => a).ToList();
            Input2 = input.Select(a => a.Item2).OrderBy(a => a).ToList();
        }

        override public void SolvePart1()
        {
            var sum = 0;
            for (var i = 0; i < Input1.Count; i++)
            {
                sum += Math.Abs(Input1[i] - Input2[i]);
            }
            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            var sum = 0;
            for (var i = 0; i < Input1.Count; i++)
            {
                sum += Input1[i] * Input2.FindAll(a => a == Input1[i]).Count();
            }
            Console.WriteLine($"Day {DayNumber} Part 2: {sum}");
        }
    }
}