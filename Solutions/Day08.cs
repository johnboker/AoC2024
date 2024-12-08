namespace AoC2024.Solutions
{
    public class Day08 : Day
    {
        public char[,] Grid { get; set; }
        public List<Antenna> Antennas { get; set; }

        public Day08(string day, bool test) : base(day, test)
        {
            Antennas = [];
            var cols = Input.First().Length;
            var rows = Input.Count;
            Grid = new char[rows, cols];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    Grid[r, c] = Input[r][c];
                    if (Grid[r, c] != '.')
                    {
                        Antennas.Add(new Antenna(r, c, Grid[r, c]));
                    }
                }
            }
            PrintGrid();
        }

        override public void SolvePart1()
        {
            var antennaPairs = (from a in Antennas
                                join b in Antennas on a.Frequency equals b.Frequency
                                where a != b
                                select new AntennaPair(a, b))
                              .GroupBy(a => a.ToString())
                              .Select(a => a.First())
                              .ToList();

            var antiNodes = antennaPairs.SelectMany(a => a.GetAntiNodes1(Grid)).Distinct().ToList();
            PrintGrid(antiNodes);

            Console.WriteLine($"Day {DayNumber} Part 1: {antiNodes.Count}");
        }

        override public void SolvePart2()
        {
            var antennaPairs = (from a in Antennas
                                join b in Antennas on a.Frequency equals b.Frequency
                                where a != b
                                select new AntennaPair(a, b))
                               .GroupBy(a => a.ToString())
                               .Select(a => a.First())
                               .ToList();

            var antiNodes = antennaPairs.SelectMany(a => a.GetAntiNodes2(Grid)).Distinct().ToList();
            PrintGrid(antiNodes);

            Console.WriteLine($"Day {DayNumber} Part 2: {antiNodes.Count}");
        }

        private void PrintGrid(List<(int R, int C)>? antiNodes = null)
        {
            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    var p = Grid[r, c];
                    if (antiNodes?.Contains((r, c)) ?? false)
                    {
                        p = '#';
                    }
                    Console.Write(p);
                }
                Console.WriteLine();
            }
        }

        public class AntennaPair(Antenna a1, Antenna a2)
        {
            public char Frequency { get; set; } = a1.Frequency;
            public Antenna A1 { get; set; } = a1;
            public Antenna A2 { get; set; } = a2;

            public override string ToString()
            {
                return $"{A1} -> {A2}";
            }

            public (int R, int C) Slope()
            {
                return (A2.Row - A1.Row, A2.Col - A1.Col);
            }

            public List<(int R, int C)> GetAntiNodes1(char[,] grid)
            {
                var slope = Slope();
                var slopeDir = (slope.C == 0 || slope.R == 0) ? 0 : (slope.R / slope.C > 0 ? 1 : -1);

                var antiNodes = new List<(int R, int C)>
                {
                    (A1.Row - slope.R, A1.Col - slope.C),
                    (A2.Row + slope.R, A2.Col + slope.C)
                };
                antiNodes = antiNodes.Where(
                    a => a.C >= 0 && a.C < grid.GetLength(1) &&
                    a.R >= 0 && a.R < grid.GetLength(0)
                ).ToList();

                return antiNodes;
            }

            public List<(int R, int C)> GetAntiNodes2(char[,] grid)
            {
                var slope = Slope();
                var slopeDir = (slope.C == 0 || slope.R == 0) ? 0 : (slope.R / slope.C > 0 ? 1 : -1);

                var m = GCM(Math.Abs(slope.R), Math.Abs(slope.C));
                slope.R /= m;
                slope.C /= m;

                List<(int R, int C)> antiNodes = [];

                var pn1 = (A1.Row, A1.Col);
                var pn2 = (A1.Row, A1.Col);

                for (; ; )
                {
                    antiNodes.Add(pn1);
                    antiNodes.Add(pn2);

                    if ((pn1.Row < 0 || pn1.Col < 0 || pn1.Col > grid.GetLength(1) || pn1.Row > grid.GetLength(0)) &&
                       (pn2.Row < 0 || pn2.Col < 0 || pn2.Col > grid.GetLength(1) || pn2.Row > grid.GetLength(0)))
                    {
                        break;
                    }

                    pn1 = (pn1.Row - slope.R, pn1.Col - slope.C);
                    pn2 = (pn2.Row + slope.R, pn2.Col + slope.C);
                }

                antiNodes = antiNodes.Where(
                    a => a.C >= 0 && a.C < grid.GetLength(1) &&
                    a.R >= 0 && a.R < grid.GetLength(0)
                ).Distinct().ToList();

                return antiNodes;
            }

            public static int GCM(int a, int b)
            {
                if (b == 0)
                    return a;
                return GCM(b, a % b);
            }
        }
    }

    public class Antenna(int row, int col, char frequency)
    {
        public int Row { get; set; } = row;
        public int Col { get; set; } = col;
        public char Frequency { get; set; } = frequency;

        public override string ToString()
        {
            return $"{Frequency}:({Row},{Col})";
        }
    }
}
