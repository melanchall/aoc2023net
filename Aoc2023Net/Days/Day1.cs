// Trebuchet?!: https://adventofcode.com/2023/day/1

namespace Aoc2023Net.Days
{
    internal sealed class Day1 : Day
    {
        private static readonly Dictionary<string, char> DigitsStrings = new()
        {
            ["one"] = '1',
            ["two"] = '2',
            ["three"] = '3',
            ["four"] = '4',
            ["five"] = '5',
            ["six"] = '6',
            ["seven"] = '7',
            ["eight"] = '8',
            ["nine"] = '9'
        };

        public override object SolvePart1() => GetCalibrationSum(false);

        public override object SolvePart2() => GetCalibrationSum(true);

        private int GetCalibrationSum(bool useStringDigits) => InputData
            .GetInputLines()
            .Select(l =>
            {
                var indexedDigits = DigitsStrings
                    .Values
                    .SelectMany(d => new[] { (d, l.IndexOf(d)), (d, l.LastIndexOf(d)) });
                if (useStringDigits)
                    indexedDigits = indexedDigits
                        .Concat(DigitsStrings
                        .SelectMany(d => new[] { (d.Value, l.IndexOf(d.Key)), (d.Value, l.LastIndexOf(d.Key)) }));

                var sortedDigits = indexedDigits
                    .Where(d => d.Item2 >= 0)
                    .OrderBy(d => d.Item2)
                    .Select(d => d.Item1)
                    .ToArray();

                return int.Parse($"{sortedDigits.First()}{sortedDigits.Last()}");
            })
            .Sum();
    }
}
