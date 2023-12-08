using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"RL

                    AAA = (BBB, CCC)
                    BBB = (DDD, EEE)
                    CCC = (ZZZ, GGG)
                    DDD = (DDD, DDD)
                    EEE = (EEE, EEE)
                    GGG = (GGG, GGG)
                    ZZZ = (ZZZ, ZZZ)", 2)]
    [DayDataPart1(@"LLR

                    AAA = (BBB, BBB)
                    BBB = (AAA, ZZZ)
                    ZZZ = (ZZZ, ZZZ)", 6)]
    [DayDataPart1(null, 12083)]
    [DayDataPart2(@"LR

                    11A = (11B, XXX)
                    11B = (XXX, 11Z)
                    11Z = (11B, XXX)
                    22A = (22B, XXX)
                    22B = (22C, 22C)
                    22C = (22Z, 22Z)
                    22Z = (22B, 22B)
                    XXX = (XXX, XXX)", 6)]
    [DayDataPart2(null, 13385272668829)]
    [TestFixture]
    public sealed class Day8Tests : DayTests<Day8Tests>
    {
    }
}
