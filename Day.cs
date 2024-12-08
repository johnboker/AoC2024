namespace AoC2024
{
    public abstract class Day
    {
        public List<string> Input { get; set; }
        public string DayNumber { get; set; }
        public Day(string day, bool test)
        {
            DayNumber = day;
            Input = File.ReadAllLines($"Inputs/input{DayNumber}{(test ? "_test" : "")}.txt").ToList();
        }

        public abstract void SolvePart1();

        public abstract void SolvePart2();
    }
}