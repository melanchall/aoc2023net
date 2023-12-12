// Cosmic Expansion: https://adventofcode.com/2023/day/11

using Aoc2023Net.Utilities;

namespace Aoc2023Net.Days
{
    internal sealed class Day11 : Day
    {
        private sealed class Galaxy
        {
            public long X { get; set; }

            public long Y { get; set; }

            public override string ToString() => $"{X} {Y}";
        }

        public override object SolvePart1() => Solve(2);

        public override object SolvePart2() => Solve(1000000);

        private long Solve(int expansionFactor)
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var galaxies = DataProvider
                .GetGridCoordinates(width, height)
                .Where(p => grid[p.X, p.Y] == '#')
                .Select(p => new Galaxy { X = p.X, Y = p.Y })
                .ToArray();

            var allX = Enumerable.Range(0, width).ToArray();
            var allY = Enumerable.Range(0, height).ToArray();

            var xOffset = 0;
            for (var x = 0; x < width; x++)
            {
                if (allY.All(y => grid[x, y] == '.'))
                {
                    foreach (var g in galaxies.Where(gg => gg.X > x + xOffset))
                    {
                        g.X += expansionFactor - 1;
                    }

                    xOffset += expansionFactor - 1;
                }
            }

            var yOffset = 0;
            for (var y = 0; y < height; y++)
            {
                if (allX.All(x => grid[x, y] == '.'))
                {
                    foreach (var g in galaxies.Where(gg => gg.Y > y + yOffset))
                    {
                        g.Y += expansionFactor - 1;
                    }

                    yOffset += expansionFactor - 1;
                }
            }

            return DataProvider
                .GetIndicesPairs(0, galaxies.Length - 1)
                .Sum(ij => Math.Abs(galaxies[ij.I].X - galaxies[ij.J].X) + Math.Abs(galaxies[ij.I].Y - galaxies[ij.J].Y));
        }
    }
}
