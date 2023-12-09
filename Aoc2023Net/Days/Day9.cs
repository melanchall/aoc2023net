// Mirage Maintenance: https://adventofcode.com/2023/day/9

namespace Aoc2023Net.Days
{
    internal sealed class Day9 : Day
    {
        public override object SolvePart1() => Solve(
            (top, bottom) => top.Add(top.Last() + bottom.Last()),
            true);

        public override object SolvePart2() => Solve(
            (top, bottom) => top.Insert(0, top.First() - bottom.First()),
            false);

        private long Solve(
            Action<List<long>, List<long>> modifyTopByBottom,
            bool extrapolateRight)
        {
            var histories = InputData
                .GetInputLines()
                .Select(l => l.Split(' ').Select(long.Parse).ToArray())
                .ToArray();

            var result = 0L;

            foreach (var history in histories)
            {
                var tree = new List<List<long>> { history.ToList() };

                while (tree.Last().Any(n => n != 0))
                {
                    var deltas = new List<long>();
                    var lastDeltas = tree.Last();

                    for (var i = 1; i < lastDeltas.Count; i++)
                    {
                        deltas.Add(lastDeltas[i] - lastDeltas[i - 1]);
                    }

                    tree.Add(deltas);
                }

                for (var i = tree.Count - 1; i > 0; i--)
                {
                    modifyTopByBottom(tree[i - 1], tree[i]);
                }

                result += extrapolateRight
                    ? tree.First().Last()
                    : tree.First().First();
            }

            return result;
        }
    }
}
