using System.Diagnostics;

namespace AoC2024
{
    public static class Timer
    { 
        public static void Time(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}\n");
        }
    }
}