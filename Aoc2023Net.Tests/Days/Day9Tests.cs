using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"0 3 6 9 12 15
                    1 3 6 10 15 21
                    10 13 16 21 30 45", 114)]
    [DayDataPart1(2038472161)]
    [DayDataPart2(@"0 3 6 9 12 15
                    1 3 6 10 15 21
                    10 13 16 21 30 45", 2)]
    [DayDataPart2(1091)]
    [TestFixture]
    public sealed class Day9Tests : DayTests<Day9Tests>
    {
    }
}
