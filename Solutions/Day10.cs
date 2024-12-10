using System.Globalization;
using HandlebarsDotNet.PathStructure;

namespace AoC2024.Solutions
{

    public class Day10 : Day
    {
        private int[,] Grid { get; set; }
        public Day10(string day, bool test) : base(day, test)
        {
            var cols = Input.First().Length;
            var rows = Input.Count();
            Grid = new int[rows, cols];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    Grid[r, c] = Input[r][c] - '0';
                }
            }
        }

        override public void SolvePart1()
        {
            var sum = 0;

            var trailHeads = GetTrailheads();

            List<List<(int R, int C)>> paths = [];
            foreach (var h in trailHeads)
            {
                List<(int R, int C)> list = [h];
                paths.Add(list);
            }

            while (paths.Count > 0 && paths.Any(a => a.Count < 10))
            {
                for (var i = 0; i < paths.Count; i++)
                {
                    var path = paths[i];
                    var currentPosition = path.Last();

                    if (Grid[currentPosition.R, currentPosition.C] == 9) continue;

                    var nextPositions = GetNextMove(currentPosition);

                    if (nextPositions.Count == 0)
                    {
                        paths.Remove(path);
                        i--;
                    }
                    else
                    {
                        if (nextPositions.Count > 1)
                        {
                            for (var j = 1; j < nextPositions.Count; j++)
                            {
                                var newPath = new List<(int R, int C)>(path);
                                newPath.Add(nextPositions[j]);
                                paths.Add(newPath);
                            }
                        }
                        path.Add(nextPositions[0]);
                    }
                }
            }

            var groups = paths.GroupBy(a=>a[0]).ToList();
            foreach(var g in groups)
            {
                var last = g.Select(a=>a.Last()).Distinct();
                sum += last.Count();
            }

            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            var trailHeads = GetTrailheads();

            List<List<(int R, int C)>> paths = [];
            foreach (var h in trailHeads)
            {
                List<(int R, int C)> list = [h];
                paths.Add(list);
            }

            while (paths.Count > 0 && paths.Any(a => a.Count < 10))
            {
                for (var i = 0; i < paths.Count; i++)
                {
                    var path = paths[i];
                    var currentPosition = path.Last();

                    if (Grid[currentPosition.R, currentPosition.C] == 9) continue;

                    var nextPositions = GetNextMove(currentPosition);

                    if (nextPositions.Count == 0)
                    {
                        paths.Remove(path);
                        i--;
                    }
                    else
                    {
                        if (nextPositions.Count > 1)
                        {
                            for (var j = 1; j < nextPositions.Count; j++)
                            {
                                var newPath = new List<(int R, int C)>(path);
                                newPath.Add(nextPositions[j]);
                                paths.Add(newPath);
                            }
                        }
                        path.Add(nextPositions[0]);
                    }
                }
            }

            Console.WriteLine($"Day {DayNumber} Part 2: {paths.Count}");
        }


        private List<(int R, int C)> GetNextMove((int R, int C) location)
        {
            var next = new List<(int R, int C)>();
            int n = Grid[location.R, location.C];

            if (location.R - 1 >= 0 && Grid[location.R - 1, location.C] - 1 == n)
            {
                next.Add((location.R - 1, location.C));
            }
            if (location.R + 1 < Grid.GetLength(0) && Grid[location.R + 1, location.C] - 1 == n)
            {
                next.Add((location.R + 1, location.C));
            }
            if (location.C - 1 >= 0 && Grid[location.R, location.C - 1] - 1 == n)
            {
                next.Add((location.R, location.C - 1));
            }
            if (location.C + 1 < Grid.GetLength(1) && Grid[location.R, location.C + 1] - 1 == n)
            {
                next.Add((location.R, location.C + 1));
            }

            return next;
        }

        private List<(int R, int C)> GetTrailheads()
        {
            List<(int R, int C)> trailheads = [];

            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    if (Grid[r, c] == 0)
                    {
                        trailheads.Add((r, c));
                    }
                }
            }

            return trailheads;
        }

        private void PrintGrid()
        {
            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    Console.Write(Grid[r, c]);
                }
                Console.WriteLine();
            }
        }
    }
}