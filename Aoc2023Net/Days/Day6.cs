// Wait For It: https://adventofcode.com/2023/day/6

using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day6 : Day
    {
        public override object SolvePart1() => Solve(false);

        public override object SolvePart2() => Solve(true);

        private int Solve(bool ignoreSpaces)
        {
            var records = GetRecords(ignoreSpaces);
            return records
                .Select(r => Enumerable
                    .Range(0, (int)r.Time + 1)
                    .Count(t => r.Time - t > r.Distance / (double)t))
                .Aggregate((r, x) => r * x);
        }

        private (long Time, long Distance)[] GetRecords(bool ignoreSpaces)
        {
            var lines = InputData.GetInputLines();
            return GetNumbers(lines.First(), ignoreSpaces)
                .Zip(GetNumbers(lines.Last(), ignoreSpaces))
                .ToArray();
        }

        private long[] GetNumbers(string line, bool ignoreSpaces) => Regex
            .Matches(ignoreSpaces ? line.Replace(" ", string.Empty) : line, @"\d+")
            .Select(m => long.Parse(m.Value))
            .ToArray();
    }
}
