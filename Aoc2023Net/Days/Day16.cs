// The Floor Will Be Lava: https://adventofcode.com/2023/day/16

namespace Aoc2023Net.Days
{
    internal sealed class Day16 : Day
    {
        private record Beam(int X, int Y, int XD, int YD);

        private static readonly Dictionary<char, Dictionary<(int XDirection, int YDirection), (int XDirection, int YDirection)[]>> BeamMovers = new()
        {
            ['-'] = new()
            {
                [(0, 1)] = new[] { (-1, 0), (1, 0) },
                [(0, -1)] = new[] { (-1, 0), (1, 0) },
            },
            ['|'] = new()
            {
                [(1, 0)] = new[] { (0, -1), (0, 1) },
                [(-1, 0)] = new[] { (0, -1), (0, 1) },
            },
            ['/'] = new()
            {
                [(1, 0)] = new[] { (0, -1) },
                [(-1, 0)] = new[] { (0, 1) },
                [(0, 1)] = new[] { (-1, 0) },
                [(0, -1)] = new[] { (1, 0) },
            },
            ['\\'] = new()
            {
                [(-1, 0)] = new[] { (0, -1) },
                [(1, 0)] = new[] { (0, 1) },
                [(0, -1)] = new[] { (-1, 0) },
                [(0, 1)] = new[] { (1, 0) },
            }
        };

        public override object SolvePart1()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();
            return Energize(grid, width, height, new Beam(0, 0, 1, 0));
        }

        public override object SolvePart2()
        {
            var (grid, width, height) = InputData.GetInputCharGrid();

            var allX = Enumerable.Range(0, width);
            var allY = Enumerable.Range(0, height);

            return Array.Empty<Beam>()
                .Concat(allX.Select(x => new Beam(x, 0, 0, 1)))
                .Concat(allX.Select(x => new Beam(x, height - 1, 0, -1)))
                .Concat(allY.Select(y => new Beam(0, y, 1, 0)))
                .Concat(allY.Select(y => new Beam(width - 1, y, -1, 0)))
                .Max(b => Energize(grid, width, height, b));
        }

        private int Energize(char[,] grid, int width, int height, Beam beam)
        {
            var queue = new Queue<Beam>();
            queue.Enqueue(beam);

            var usedBeams = new HashSet<Beam> { beam };

            while (queue.Any())
            {
                var (x, y, xd, yd) = queue.Dequeue();

                var c = grid[x, y];
                if (!BeamMovers.TryGetValue(c, out var d) || !d.TryGetValue((xd, yd), out var directions))
                    directions = new[] { (xd, yd) };

                foreach (var (xDirection, yDirection) in directions)
                {
                    var newBeam = new Beam(x + xDirection, y + yDirection, xDirection, yDirection);
                    if (newBeam.X >= 0 && newBeam.X < width && newBeam.Y >= 0 && newBeam.Y < height && usedBeams.Add(newBeam))
                        queue.Enqueue(newBeam);
                }
            }

            return usedBeams.Select(b => (b.X, b.Y)).Distinct().Count();
        }
    }
}
