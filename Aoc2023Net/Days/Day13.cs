// Point of Incidence: https://adventofcode.com/2023/day/13

using Aoc2023Net.Utilities;

namespace Aoc2023Net.Days
{
    internal sealed class Day13 : Day
    {
        private enum LineType
        {
            Vertical,
            Horizontal
        }

        private record Line(LineType Type, int Value);

        public override object SolvePart1()
        {
            var grids = GetGrids();
            var result = 0L;

            foreach (var (grid, width, height) in grids)
            {
                var line = GetLines(grid, width, height).First();
                result += Summarize(line);
            }

            return result;
        }

        public override object SolvePart2()
        {
            var grids = GetGrids();
            var result = 0L;

            foreach (var (grid, width, height) in grids)
            {
                var lines = GetLines(grid, width, height);

                foreach (var (x, y) in DataProvider.GetGridCoordinates(width, height))
                {
                    var oldSymbol = grid[x, y];
                    grid[x, y] = grid[x, y] == '.' ? '#' : '.';
                    
                    var newLine = GetLines(grid, width, height).Except(lines).FirstOrDefault();
                    if (newLine != null)
                    {
                        result += Summarize(newLine);
                        break;
                    }

                    grid[x, y] = oldSymbol;
                }
            }

            return result;
        }

        private int Summarize(Line line) => line.Type == LineType.Vertical
            ? line.Value + 1
            : (line.Value + 1) * 100;

        private (char[,] Grid, int Width, int Height)[] GetGrids() => InputData
            .GetInputLinesGroups()
            .Select(g =>
            {
                var width = g[0].Length;
                var height = g.Length;
                var grid = new char[width, height];

                DataProvider.GetGridCoordinates(width, height).ToList().ForEach(p => grid[p.X, p.Y] = g[p.Y][p.X]);

                return (grid, width, height);
            })
            .ToArray();

        private Line[] GetLines(char[,] grid, int width, int height)
        {
            var result = new List<Line>();

            result.AddRange(Foo(grid, width, height, LineType.Vertical));
            result.AddRange(Foo(grid, width, height, LineType.Horizontal));

            return [.. result];
        }

        private Line[] Foo(char[,] grid, int width, int height, LineType lineType)
        {
            var result = new List<Line>();

            for (var lineCoordinate = 0; lineCoordinate < (lineType == LineType.Vertical ? width : height) - 1; lineCoordinate++)
            {
                var lineFound = true;

                for (var crossLineCoordinate = 0; crossLineCoordinate < (lineType == LineType.Vertical ? height : width) && lineFound; crossLineCoordinate++)
                {
                    var aSide = Enumerable
                        .Range(0, lineCoordinate + 1)
                        .Select(i => lineType == LineType.Vertical ? grid[i, crossLineCoordinate] : grid[crossLineCoordinate, i])
                        .ToArray();
                    var bSide = Enumerable
                        .Range(lineCoordinate + 1, (lineType == LineType.Vertical ? width : height) - lineCoordinate - 1)
                        .Select(i => lineType == LineType.Vertical ? grid[i, crossLineCoordinate] : grid[crossLineCoordinate, i])
                        .ToArray();

                    if (aSide.Length < bSide.Length)
                        bSide = bSide.Take(aSide.Length).ToArray();
                    else
                        aSide = aSide.Skip(aSide.Length - bSide.Length).ToArray();

                    lineFound = aSide.SequenceEqual(bSide.Reverse());
                }

                if (lineFound)
                    result.Add(new Line(lineType, lineCoordinate));
            }

            return [.. result];
        }
    }
}
