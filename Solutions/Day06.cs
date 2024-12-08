namespace AoC2024.Solutions
{
    public class Day06 : Day
    {
        private char[,] Grid { get; set; }

        public Day06(string day, bool test) : base(day, test)
        {
            var cols = Input.First().Length;
            var rows = Input.Count();
            Grid = new char[rows, cols];
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    Grid[r, c] = Input[r][c];
                }
            }
            PrintGrid();
        }

        override public void SolvePart1()
        {
            var visited = new HashSet<(int R, int C, char D)>();

            var position = GetCurrentPosition();
            for (; ; )
            {
                visited.Add(position);
                var next = GetNextPosition(position, visited);
                if (next.D == 'O') break;
                position = next;
            }
            Console.WriteLine($"Day {DayNumber} Part 1: {visited.Select(a => (a.R, a.C)).Distinct().Count()}");
        }

        override public void SolvePart2()
        {
            var start = GetCurrentPosition();
            var obstacles = new List<(int R, int C)>();

            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    if (Grid[r, c] == '#' || (start.R == r && start.C == c)) continue;

                    Grid[r,c] = '#';

                    var visited = new HashSet<(int R, int C, char D)>();
                    var position = start;
                    for (; ; )
                    {
                        visited.Add(position);
                        var next = GetNextPosition(position, visited);
                        if (next.D == 'O') break;
                        if (next.D == 'L')
                        {
                            obstacles.Add((r, c));
                            break;
                        }
                        position = next;
                    }

                    Grid[r,c] = '.';
                }
            }
            Console.WriteLine($"Day {DayNumber} Part 2: {obstacles.Count}");
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

        private (int R, int C, char D) GetNextPosition((int R, int C, char D) position, HashSet<(int R, int C, char D)> visited)
        {
            var delta = GetDelta(position.D);
            var next = (R: position.R + delta.RDelta, C: position.C + delta.CDelta, D: position.D);
            if (visited.Contains(next)) return (-1, -1, 'L');

            if (next.R < 0 || next.C < 0 ||
                next.R > Grid.GetLength(0) - 1 || next.C > Grid.GetLength(1) - 1)
            {
                return (-1, -1, 'O');
            }

            if (Grid[next.R, next.C] == '#')
            {
                return GetNextPosition((position.R, position.C, Rotate90(position.D)), visited);
            }

            return next;
        }

        private char Rotate90(char d)
        {
            return d switch
            {
                '^' => '>',
                'v' => '<',
                '>' => 'v',
                '<' => '^',
                _ => '?'
            };
        }

        private (int R, int C, char D) GetCurrentPosition()
        {
            var guardCharacters = new char[] { '^', '>', '<', 'v' };
            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    var g = Grid[r, c];
                    if (guardCharacters.Contains(g))
                    {
                        return (r, c, g);
                    }
                }
            }
            return (-1, -1, '>');
        }

        private (int RDelta, int CDelta) GetDelta(char g)
        {
            return g switch
            {
                '^' => (-1, 0),
                'v' => (1, 0),
                '>' => (0, 1),
                '<' => (0, -1),
                _ => (0, 0)
            };
        }
    }
}