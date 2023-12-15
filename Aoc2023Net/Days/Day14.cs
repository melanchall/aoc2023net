// Parabolic Reflector Dish: https://adventofcode.com/2023/day/14

using Aoc2023Net.Utilities;

namespace Aoc2023Net.Days
{
    internal sealed class Day14 : Day
    {
        private sealed class Point
        {
            public int X { get; set; }

            public int Y { get; set; }
        }

        public override object SolvePart1()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            var xys = DataProvider
                .GetGridCoordinates(width, height)
                .Where(xy => grid[xy.X, xy.Y] == 'O')
                .Select(xy => new Point { X = xy.X, Y = xy.Y })
                .OrderBy(p => p.Y)
                .ThenBy(p => p.X)
                .ToArray();

            foreach (var p in xys)
            {
                var newY = Enumerable
                    .Range(0, p.Y)
                    .Reverse()
                    .TakeWhile(y => grid[p.X, y] == '.' && !xys.Any(xy => xy.X == p.X && xy.Y == y))
                    .LastOrDefault(-1);

                if (newY < 0)
                    continue;

                grid[p.X, p.Y] = '.';
                p.Y = newY;
            }

            return xys.Sum(p => height - p.Y);
        }

        public override object SolvePart2() => null;
    }
}
