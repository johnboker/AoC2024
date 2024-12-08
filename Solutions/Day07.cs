using System.Numerics;

namespace AoC2024.Solutions
{
    public class Day07 : Day
    {

        public Day07(string day, bool test) : base(day, test)
        {

        }

        override public void SolvePart1()
        {
            BigInteger sum = 0;

            foreach (var line in Input)
            {
                var test = new Test(line);
                if (test.Evaluate())
                {
                    sum += test.Value;
                }
            }

            Console.WriteLine($"Day {DayNumber} Part 1: {sum}");
        }

        override public void SolvePart2()
        {
            BigInteger sum = 0;

            foreach (var line in Input)
            {
                var test = new Test(line);
                if (test.Evaluate2())
                {
                    sum += test.Value;
                }
            }

            Console.WriteLine($"Day {DayNumber} Part 2: {sum}");
        }


        private class Test
        {
            public BigInteger Value { get; set; }
            public List<BigInteger> Numbers { get; set; }


            public Test(string line)
            {
                var parts = line.Split(":");
                Value = BigInteger.Parse(parts[0]);
                Numbers = parts[1]
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(a => BigInteger.Parse(a.Trim()))
                            .ToList();
            }

            public bool Evaluate()
            {
                for (int i = 0; i <= Math.Pow(2, Numbers.Count - 1) - 1; i++)
                {
                    var a = Evaluate(i);
                    if (a) return true;
                }

                return false;
            }

            public bool Evaluate(int opbits)
            {
                BigInteger t = Numbers[0];
                for (int i = 1; i < Numbers.Count; i++)
                {
                    var op = ((opbits >> i - 1) & 1) == 0 ? '+' : '*';

                    if (op == '+') t += Numbers[i];
                    else if (op == '*') t *= Numbers[i];

                    if(t > Value) return false;
                }
                return t == Value;

            }

            public bool Evaluate2()
            {
                for (int i = 0; i <= Math.Pow(3, Numbers.Count - 1) - 1; i++)
                {
                    var a = Evaluate2(Int32ToString(i, 3));
                    if (a) return true;
                }

                return false;
            }

            public bool Evaluate2(string opbits)
            {
                BigInteger t = Numbers[0];
                for (int i = 1; i < Numbers.Count; i++)
                {
                    var op = opbits[opbits.Length - i] switch
                    {
                        '0' => '+',
                        '1' => '*',
                        '2' => '|',
                        _ => 'x'
                    };

                    if (op == '+') t += Numbers[i];
                    else if (op == '*') t *= Numbers[i];
                    else if (op == '|') t = BigInteger.Parse($"{t}{Numbers[i]}");

                    if(t > Value) return false;
                }
                return t == Value;
            }
        }

        public static string Int32ToString(int value, int toBase)
        {
            string result = string.Empty;
            do
            {
                result = "0123456789ABCDEF"[value % toBase] + result;
                value /= toBase;
            }
            while (value > 0);

            return result.PadLeft(20, '0');
        }
    }
}