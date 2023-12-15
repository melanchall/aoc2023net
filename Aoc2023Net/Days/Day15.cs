// Lens Library: https://adventofcode.com/2023/day/15

using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day15 : Day
    {
        private sealed class Lens
        {
            public string Label { get; set; }

            public int? FocalLength { get; set; }
        }

        public override object SolvePart1() => InputData
            .GetInputText()
            .Split(',')
            .Sum(Hash);

        public override object SolvePart2()
        {
            var instructions = InputData
                .GetInputText()
                .Split(',')
                .Select(i => Regex.Match(i, @"(\w+)(=(?<f>\d)|-)"))
                .Select(m => (
                    Label: m.Groups[1].Value,
                    FocalLength: int.TryParse(m.Groups["f"].Value, out var f) ? (int?)f : null))
                .ToArray();

            var boxes = Enumerable.Range(0, 256).Select(_ => new List<Lens>()).ToArray();

            foreach (var (label, focalLength) in instructions)
            {
                var box = boxes[Hash(label)];

                if (focalLength == null)
                {
                    box.RemoveAll(l => l.Label == label);
                    continue;
                }

                var existingLens = box.FirstOrDefault(l => l.Label == label);
                if (existingLens != null)
                    existingLens.FocalLength = focalLength;
                else
                    box.Add(new Lens
                    {
                        Label = label,
                        FocalLength = focalLength
                    });
            }

            return boxes
                .Select((b, boxIndex) => b
                    .Select((l, lensIndex) => (boxIndex + 1) * (lensIndex + 1) * l.FocalLength)
                    .Sum())
                .Sum();
        }

        private long Hash(string s) => Encoding
            .ASCII
            .GetBytes(s)
            .Aggregate(0L, (result, asciiByte) => (result + asciiByte) * 17 % 256);
    }
}
