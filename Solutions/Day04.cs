namespace AoC2024.Solutions
{
    public class Day04 : Day
    {
        private char[,] Grid { get; set; }
        public Day04(string day, bool test) : base(day, test)
        {
            var cols = Input.First().Length;
            var rows = Input.Count();
            Grid = new char[rows, cols];
            var lines = Input.ToArray();
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    Grid[r, c] = lines[r][c];
                }
            }
        }

        override public void SolvePart1()
        {
            var cnt = 0;
            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    cnt += CountFoundAtPosition(r, c);
                }

            }

            Console.WriteLine($"Day {DayNumber} Part 1: {cnt}");
        }

        override public void SolvePart2()
        {
             var cnt = 0;
            for (var r = 0; r < Grid.GetLength(0); r++)
            {
                for (var c = 0; c < Grid.GetLength(1); c++)
                {
                    cnt += FindXMAS(r, c);
                }

            }
            Console.WriteLine($"Day {DayNumber} Part 2: {cnt}");
        }

        private int FindXMAS(int row, int col)
        {
            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);

            if (Grid[row, col] != 'A' || row == 0 || col == 0 || row == rows - 1 || col == cols - 1) return 0;

            return 
// M.S
// .A.
// M.S
                   (Grid[row - 1, col - 1] == 'M' &&
                   Grid[row - 1, col + 1] == 'S' &&
                   Grid[row + 1, col - 1] == 'M' &&
                   Grid[row + 1, col + 1] == 'S') ||
// S.M
// .A.
// S.M
                   (Grid[row - 1, col - 1] == 'S' &&
                   Grid[row - 1, col + 1] == 'M' &&
                   Grid[row + 1, col - 1] == 'S' &&
                   Grid[row + 1, col + 1] == 'M') ||
// M.M
// .A.
// S.S
                   (Grid[row - 1, col - 1] == 'M' &&
                   Grid[row - 1, col + 1] == 'M' &&
                   Grid[row + 1, col - 1] == 'S' &&
                   Grid[row + 1, col + 1] == 'S') ||
// M.M
// .A.
// S.S
                   (Grid[row - 1, col - 1] == 'S' &&
                   Grid[row - 1, col + 1] == 'S' &&
                   Grid[row + 1, col - 1] == 'M' &&
                   Grid[row + 1, col + 1] == 'M')         
                   
                   ? 1 : 0;
        }

        private int CountFoundAtPosition(int row, int col)
        {
            if (Grid[row, col] != 'X') return 0;
            var words = new List<string>();

            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);

            // h - l to r
            var word = new List<char>();
            for (var c = 0; c < 4 && col + c < cols; c++)
            {
                if ("XMAS"[c] == Grid[row, col + c])
                {
                    word.Add(Grid[row, col + c]);
                }
            }
            words.Add(string.Join("", word));

            // h - r to l
            word = new List<char>();
            for (var c = 0; c < 4 && col - c >= 0; c++)
            {
                if ("XMAS"[c] == Grid[row, col - c])
                {
                    word.Add(Grid[row, col - c]);
                }
            }
            words.Add(string.Join("", word));

            // v - t to b
            word = new List<char>();
            for (var r = 0; r < 4 && row + r < rows; r++)
            {
                if ("XMAS"[r] == Grid[row + r, col])
                {
                    word.Add(Grid[row + r, col]);
                }
            }
            words.Add(string.Join("", word));

            // v - b to t
            word = new List<char>();
            for (var r = 0; r < 4 && row - r >= 0; r++)
            {
                if ("XMAS"[r] == Grid[row - r, col])
                {
                    word.Add(Grid[row - r, col]);
                }
            }
            words.Add(string.Join("", word));


            // dt - l to r
            word = new List<char>();
            for (var i = 0; i < 4 && col + i < cols && row + i < rows; i++)
            {
                if ("XMAS"[i] == Grid[row + i, col + i])
                {
                    word.Add(Grid[row + i, col + i]);
                }
            }
            words.Add(string.Join("", word));


            // dt - r to l
            word = new List<char>();
            for (var i = 0; i < 4 && col - i >= 0 && row + i < rows; i++)
            {
                if ("XMAS"[i] == Grid[row + i, col - i])
                {
                    word.Add(Grid[row + i, col - i]);
                }
            }
            words.Add(string.Join("", word));

            // ub - l to r
            word = new List<char>();
            for (var i = 0; i < 4 && row - i >= 0 && col + i < cols; i++)
            {
                if ("XMAS"[i] == Grid[row - i, col + i])
                {
                    word.Add(Grid[row - i, col + i]);
                }
            }
            words.Add(string.Join("", word));

            // ub - r to l
            word = new List<char>();
            for (var i = 0; i < 4 && row - i >= 0 && col - i >= 0; i++)
            {
                if ("XMAS"[i] == Grid[row - i, col - i])
                {
                    word.Add(Grid[row - i, col - i]);
                }
            }
            words.Add(string.Join("", word));

            return words.Where(a => a == "XMAS").Count();
        }
    }
}