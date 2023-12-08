// Haunted Wasteland: https://adventofcode.com/2023/day/8

using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day8 : Day
    {
        private record NodeChildren(string Left, string Right);

        public override object SolvePart1() => Solve(
            n => n == "AAA",
            n => n == "ZZZ");

        public override object SolvePart2() => Solve(
            n => n.EndsWith('A'),
            n => n.EndsWith('Z'));

        private long Solve(Func<string, bool> startNodesSelector, Predicate<string> endNoteSelector)
        {
            var (instructions, map) = GetInstructionAndMap();

            var nodes = map.Keys.Where(startNodesSelector).ToArray();
            var nodesSteps = new long[nodes.Length];
            var steps = 0;

            while (true)
            {
                foreach (var i in instructions)
                {
                    steps++;

                    for (var j = 0; j < nodes.Length; j++)
                    {
                        nodes[j] = i == 'L' ? map[nodes[j]].Left : map[nodes[j]].Right;
                        if (endNoteSelector(nodes[j]) && nodesSteps[j] == 0)
                            nodesSteps[j] = steps;
                    }

                    if (nodesSteps.All(n => n > 0))
                        return GetLeastCommonMultiple(nodesSteps);
                }
            }
        }

        private (string Instruction, Dictionary<string, NodeChildren> Map) GetInstructionAndMap()
        {
            var groups = InputData.GetInputLinesGroups();

            var instruction = groups[0].First();
            var map = groups[1]
                .Select(l => Regex.Match(l, @"(\w+)\s+=\s+\((\w+),\s+(\w+)\)", RegexOptions.IgnoreCase))
                .ToDictionary(
                    m => m.Groups[1].Value,
                    m => new NodeChildren(m.Groups[2].Value, m.Groups[3].Value));

            return (instruction, map);
        }

        // https://stackoverflow.com/a/29717490/2975589

        private static long GetLeastCommonMultiple(long[] numbers) =>
            numbers.Aggregate((a, b) => Math.Abs(a * b) / GetGreatestCommonDivisor(a, b));

        private static long GetGreatestCommonDivisor(long a, long b) =>
            b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
    }
}
