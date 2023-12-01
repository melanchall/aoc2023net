using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@"1abc2
                    pqr3stu8vwx
                    a1b2c3d4e5f
                    treb7uchet", 142)]
    [DayDataPart1(null, 53921)]
    [DayDataPart2(@"two1nine
                    eightwothree
                    abcone2threexyz
                    xtwone3four
                    4nineeightseven2
                    zoneight234
                    7pqrstsixteen", 281)]
    [DayDataPart2(null, 54676)]
    [TestFixture]
    public sealed class Day1Tests : DayTests<Day1Tests>
    {
    }
}
