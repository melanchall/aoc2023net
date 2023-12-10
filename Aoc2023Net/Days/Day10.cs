// Pipe Maze: https://adventofcode.com/2023/day/10

using Aoc2023Net.Utilities;

namespace Aoc2023Net.Days
{
    internal sealed class Day10 : Day
    {
        // Describes how direction vector should be modified by a tile
        private static readonly Dictionary<char, Dictionary<(int X, int Y), (int X, int Y)>> TileTurns = new()
        {
            ['|'] = new()
            {
                [(0, 1)] = (0, 1),
                [(0, -1)] = (0, -1),
            },
            ['-'] = new()
            {
                [(1, 0)] = (1, 0),
                [(-1, 0)] = (-1, 0),
            },
            ['L'] = new()
            {
                [(0, 1)] = (1, 0),
                [(-1, 0)] = (0, -1),
            },
            ['J'] = new()
            {
                [(0, 1)] = (-1, 0),
                [(1, 0)] = (0, -1),
            },
            ['7'] = new()
            {
                [(0, -1)] = (-1, 0),
                [(1, 0)] = (0, 1),
            },
            ['F'] = new()
            {
                [(0, -1)] = (1, 0),
                [(-1, 0)] = (0, 1),
            },
            ['.'] = []
        };

        public override object SolvePart1() => GetLoop().Count / 2;

        public override object SolvePart2() => null;

        private HashSet<(int X, int Y)> GetLoop()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();

            var startPoint = DataProvider
                .GetGridCoordinates(width, height)
                .First(p => grid[p.X, p.Y] == 'S');

            var direction = new (int X, int Y)[] { (-1, 0), (1, 0), (0, -1), (0, 1) }
                .First(p =>
                {
                    var (x, y) = (startPoint.X + p.X, startPoint.Y + p.Y);
                    return
                        x >= 0 &&
                        x < width &&
                        y >= 0 &&
                        y < height &&
                        TileTurns[grid[x, y]].ContainsKey(p);
                });

            var point = startPoint;
            var path = new HashSet<(int X, int Y)> { startPoint  };

            while (true)
            {
                point = (point.X + direction.X, point.Y + direction.Y);
                if (!path.Add(point))
                    break;

                direction = TileTurns[grid[point.X, point.Y]][direction];
            }

            return path;
        }
    }
}
