using System.Numerics;

namespace AoC2024.Solutions
{
    public class Day09 : Day
    {
        private string DiskMap { get; set; }
        public Day09(string day, bool test) : base(day, test)
        {
            DiskMap = Input[0];
        }

        override public void SolvePart1()
        {
            var disk = GetDiskBlocks();

            var left = 0;
            var right = disk.Length - 1;

            do
            {
                var left_content = disk[left];
                var right_content = disk[right];

                if (left_content == -1 && right_content != -1)
                {
                    disk[left] = right_content;
                    disk[right] = -1;
                    left++;
                    right--;
                }
                else if (left_content != -1)
                {
                    left++;
                }
                else if (right_content == -1)
                {
                    right--;
                }

            } while (left < right);

            var checksum = Checksum(disk);

            Console.WriteLine($"Day {DayNumber} Part 1: {checksum}");
        }

        override public void SolvePart2()
        {
            var disk = GetDiskBlocks();

            var workingFile = DiskMap.Length / 2;

            do
            {
                var workingFileLocation = GetFile(disk, workingFile);
                var to = GetFreeLocationForSize(disk, workingFileLocation.length);
                if (to.startIndex > -1 && to.startIndex < workingFileLocation.startIndex)
                {
                    MoveFile(disk, workingFileLocation, to);
                }
                workingFile--;

            } while (workingFile >= 0);

            var checksum = Checksum(disk);

            Console.WriteLine($"Day {DayNumber} Part 2: {checksum}");
        }

        public void MoveFile(int[] disk, (int start, int length) from, (int start, int lengh) to)
        {
            for (var i = 0; i < from.length; i++)
            {
                disk[to.start + i] = disk[from.start + i];
                disk[from.start + i] = -1;
            }
        }

        public (int startIndex, int length) GetFreeLocationForSize(int[] disk, int size)
        {
            var startIndex = -1;
            var length = 0;

            for (var i = 0; i < disk.Length; i++)
            {
                var found = disk[i] == -1;

                if (found && startIndex == -1)
                {
                    startIndex = i;
                }

                if (found)
                {
                    length++;
                }

                if (!found && startIndex > -1)
                {
                    if (length >= size)
                    {
                        break;
                    }
                    else
                    {
                        startIndex = -1;
                        length = 0;
                    }
                }
            }

            return length >= size ? (startIndex, length) : (-1, -1);
        }

        public (int startIndex, int length) GetFile(int[] disk, int file)
        {
            var startIndex = -1;
            var length = 0;
            for (var i = 0; i < disk.Length; i++)
            {
                var found = disk[i] == file;
                if (found && startIndex == -1)
                {
                    startIndex = i;
                }
                if (found)
                {
                    length++;
                }
                if (!found && startIndex > -1) break;
            }

            return (startIndex, length);
        }

        public BigInteger Checksum(int[] disk)
        {
            BigInteger sum = 0;
            for (var i = 0; i < disk.Length; i++)
            {
                sum += disk[i] == -1 ? 0 : (disk[i] * i);
            }
            return sum;
        }

        public void PrintDisk(int[] disk)
        {
            foreach (var b in disk)
            {
                var output = b == -1 ? '.' : (char)(b + '0');
                Console.Write(output);
            }
            Console.WriteLine();
        }

        public int[] GetDiskBlocks()
        {
            var disk = new List<int>();

            for (int i = 0; i < DiskMap.Length; i++)
            {
                var blockCount = DiskMap[i] - '0';
                for (var j = 0; j < blockCount; j++)
                {
                    var block = i % 2 == 0 ? (i / 2) : -1;
                    disk.Add(block);
                }
            }

            return disk.ToArray();
        }
    }
}