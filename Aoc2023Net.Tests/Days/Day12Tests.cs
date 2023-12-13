using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"???.### 1,1,3
                    .??..??...?##. 1,1,3
                    ?#?#?#?#?#?#?#? 1,3,1,6
                    ????.#...#... 4,1,1
                    ????.######..#####. 1,6,5
                    ?###???????? 3,2,1", 21)]
    [DayDataPart1(7236)]
    [DayDataPart2(@"???.### 1,1,3
                    .??..??...?##. 1,1,3
                    ?#?#?#?#?#?#?#? 1,3,1,6
                    ????.#...#... 4,1,1
                    ????.######..#####. 1,6,5
                    ?###???????? 3,2,1", 525152)]
    [DayDataPart2(null)]
    [TestFixture]
    public sealed class Day12Tests : DayTests<Day12Tests>
    {
    }
}
