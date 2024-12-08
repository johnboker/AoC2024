using System.Data;

namespace AoC2024.Solutions
{
    public class Day05 : Day
    {
        private List<(int A, int B)> Rules { get; set; }
        private List<List<int>> Pages { get; set; }
        public Day05(string day, bool test) : base(day, test)
        {
            Rules = [];
            Pages = [];
            int i = 0;
            while (Input[i] != "")
            {
                var parts = Input[i].Split("|");
                Rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                i++;
            }
            i++;

            while (i < Input.Count)
            {
                Pages.Add(Input[i].Split(",").Select(a => int.Parse(a)).ToList());
                i++;
            }

        }

        override public void SolvePart1()
        {
            var sum = 0;

            foreach (var pages in Pages)
            {
                var isValid = ValidatePage(pages);
                if (isValid)
                {
                    Console.WriteLine($"{string.Join(",", pages)}-> {pages[pages.Count / 2]}");
                    sum += pages[pages.Count / 2];
                }
            }


            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            var sum = 0;
            foreach (var pages in Pages)
            {
                var isValid = ValidatePage(pages);
                if (!isValid)
                {
                    FixOrder(pages);
                    Console.WriteLine($"{string.Join(",", pages)}-> {pages[pages.Count / 2]}");
                    sum += pages[pages.Count / 2];
                }
            }
            Console.WriteLine($"Day {DayNumber} Part 2: {sum}");
        }

        private void FixOrder(List<int> pages)
        {
            foreach (var page in pages)
            {
                var breakLoop = false;
                foreach (var rule in Rules)
                {
                    if (!ValidatePageRule(page, rule, pages))
                    {
                        var index1 = pages.FindIndex(a => a == rule.A);
                        var index2 = pages.FindIndex(a => a == rule.B);
                        if (index1 >= 0 && index2 >= 0)
                        {
                            var t = pages[index1];
                            pages[index1] = pages[index2];
                            pages[index2] = t;
                            FixOrder(pages);
                            breakLoop = true;
                            break;
                        }
                    }
                }
                if(breakLoop) break;
            }
        }

        private bool ValidatePage(List<int> pages)
        {
            for (var i = 0; i < pages.Count; i++)
            {
                var pageNumber = pages[i];
                var rules = Rules.Where(a => a.A == pageNumber || a.B == pageNumber).ToList();
                foreach (var rule in rules)
                {
                    var pageRuleValid = ValidatePageRule(pageNumber, rule, pages);
                    if (!pageRuleValid) return false;
                }
            }

            return true;
        }

        private bool ValidatePageRule(int pageNumber, (int A, int B) rule, List<int> pages)
        {
            var p = string.Join("|", pages.Where(a => a == rule.A || a == rule.B));
            var valid = p == $"{rule.A}|{rule.B}" || p == $"{rule.A}" || p == $"{rule.B}";
            return valid;
        }

    }
}