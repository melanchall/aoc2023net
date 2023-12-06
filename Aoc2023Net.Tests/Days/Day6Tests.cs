using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"Time:      7  15   30
                    Distance:  9  40  200", 288)]
    [DayDataPart1(null, 633080)]
    [DayDataPart2(@"Time:      7  15   30
                    Distance:  9  40  200", 71503)]
    [DayDataPart2(null, 20048741)]
    [TestFixture]
    public sealed class Day6Tests : DayTests<Day6Tests>
    {
    }
}
