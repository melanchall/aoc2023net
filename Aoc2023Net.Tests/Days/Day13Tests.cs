using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"#.##..##.
                    ..#.##.#.
                    ##......#
                    ##......#
                    ..#.##.#.
                    ..##..##.
                    #.#.##.#.

                    #...##..#
                    #....#..#
                    ..##..###
                    #####.##.
                    #####.##.
                    ..##..###
                    #....#..#", 405)]
    [DayDataPart1(33047)]
    [DayDataPart2(@"#.##..##.
                    ..#.##.#.
                    ##......#
                    ##......#
                    ..#.##.#.
                    ..##..##.
                    #.#.##.#.

                    #...##..#
                    #....#..#
                    ..##..###
                    #####.##.
                    #####.##.
                    ..##..###
                    #....#..#", 400)]
    [DayDataPart2(28806)]
    [TestFixture]
    public sealed class Day13Tests : DayTests<Day13Tests>
    {
    }
}
