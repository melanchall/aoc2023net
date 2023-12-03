// Gear Ratios: https://adventofcode.com/2023/day/3

using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day3 : Day
    {
        private record PartNumber(int Number, int X, int Y, int Length);

        public override object SolvePart1()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            return GetPartsNumbers(grid, width, height).Sum(n => n.Number);
        }

        public override object SolvePart2()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var partsNumbers = GetPartsNumbers(grid, width, height);

            var gears = new Dictionary<(int X, int Y), List<PartNumber>>();

            foreach (var partNumber in partsNumbers)
            {
                IterateFrame(
                    partNumber.X, partNumber.X + partNumber.Length, partNumber.Y, width, height,
                    (frameX, frameY) =>
                    {
                        if (grid[frameX, frameY] == '*')
                        {
                            if (!gears.TryGetValue((frameX, frameY), out var gearPartsNumbers))
                                gears.Add((frameX, frameY), gearPartsNumbers = new List<PartNumber>());

                            gearPartsNumbers.Add(partNumber);
                        }
                    });
            }

            return gears
                .Values
                .Where(t => t.Count == 2)
                .Sum(t => t[0].Number * t[1].Number);
        }

        private List<PartNumber> GetPartsNumbers(char[,] grid, int width, int height)
        {
            var markers = new bool[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var c = grid[x, y];
                    if (c == '.' || char.IsDigit(c))
                        continue;

                    IterateFrame(
                        x, x + 1, y, width, height,
                        (frameX, frameY) => markers[frameX, frameY] = true);
                }
            }

            var lines = InputData.GetInputLines();
            var result = new List<PartNumber>();

            for (var y = 0; y < lines.Length; y++)
            {
                foreach (Match m in Regex.Matches(lines[y], @"\d+"))
                {
                    if (Enumerable.Range(m.Index, m.Length).Any(x => markers[x, y]))
                        result.Add(new (int.Parse(m.Value), m.Index, y, m.Length));
                }
            }

            return result;
        }

        private void IterateFrame(int startX, int endX, int y, int gridWidth, int gridHeight, Action<int, int> action)
        {
            for (var frameX = startX - 1; frameX <= endX; frameX++)
            {
                for (var frameY = y - 1; frameY <= y + 1; frameY++)
                {
                    if (IsInGrid(frameX, frameY, gridWidth, gridHeight))
                    {
                        action(frameX, frameY);
                    }
                }
            }
        }

        private bool IsInGrid(int x, int y, int gridWidth, int gridHeight) =>
            x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }
}
