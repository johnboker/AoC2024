using Microsoft.Extensions.Configuration;
using System.Net;
using HandlebarsDotNet;

namespace AoC2024
{
    class Program
    {
        static async Task Main(string[] args)
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
                await GenerateFiles(dayString);
            }
        }

        private static async Task GenerateFiles(string dayString)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile($"appsettings.local.json", true, true);

            var config = builder.Build();
            var sessionCookieValue = config["aoc:session"];

            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            cookieContainer.Add(new Uri("https://adventofcode.com"), new Cookie("session", sessionCookieValue));
            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("boker.dev input retriever 1.0");
            var content = await httpClient.GetStringAsync($"https://adventofcode.com/2024/day/{int.Parse(dayString)}/input");

            using var inputWriter = File.CreateText($"Inputs/input{dayString}.txt");
            inputWriter.Write(content);

            using var s2 = File.Create($"Inputs/input{dayString}_test.txt");

            var template = Handlebars.Compile(File.ReadAllText("Templates/Day.hbs"));
            using var writer = File.CreateText($"Solutions/Day{dayString}.cs");
            writer.Write(template(new { DayString = dayString }));
        }
    }
}