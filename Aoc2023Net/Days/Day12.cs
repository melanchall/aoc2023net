// Hot Springs: https://adventofcode.com/2023/day/12

using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day12 : Day
    {
        public override object SolvePart1()
        {
            var rows = InputData
                .GetInputLines()
                .Select(l => l.Split(' '))
                .Select(p => (
                    SpringsPattern: p[0],
                    SpringsGroupsSizes: p[1].Split(',').Select(int.Parse).ToArray()))
                .ToArray();

            var result = 0L;

            foreach (var (springsPattern, springsGroupsSizes) in rows)
            {
                var springsRegex = new Regex(springsPattern.Replace(".", @"\.").Replace("?", "."));

                // [0, 1, 1, ... 1]
                // 1 for min space between damaged groups, 0 allowed for the first group
                var stepsBeforeDamagedGroups = new int[springsGroupsSizes.Length];
                Array.Fill(stepsBeforeDamagedGroups, 1);
                stepsBeforeDamagedGroups[0] = 0;

                var thereAreArrangements = true;

                while (thereAreArrangements)
                {
                    var arrangement = string
                        .Join(
                            string.Empty,
                            Enumerable
                                .Range(0, springsGroupsSizes.Length)
                                .Select(i => new string('.', stepsBeforeDamagedGroups[i]) + new string('#', springsGroupsSizes[i])))
                        .PadRight(springsPattern.Length, '.');
                    
                    if (springsRegex.IsMatch(arrangement))
                        result++;

                    stepsBeforeDamagedGroups[stepsBeforeDamagedGroups.Length - 1]++;

                    // if we went beyond pattern length, try another arrangement
                    if (springsGroupsSizes.Sum() + stepsBeforeDamagedGroups.Sum() > springsPattern.Length)
                    {
                        for (var j = stepsBeforeDamagedGroups.Length - 1; j >= 0; j--)
                        {
                            stepsBeforeDamagedGroups[j]++;
                            if (springsGroupsSizes.Sum() + stepsBeforeDamagedGroups.Sum() <= springsPattern.Length)
                                break;

                            stepsBeforeDamagedGroups[j] = 1;
                            if (j == 0)
                                thereAreArrangements = false;
                        }
                    }
                }
            }

            return result;
        }

        public override object SolvePart2() => null;
    }
}
