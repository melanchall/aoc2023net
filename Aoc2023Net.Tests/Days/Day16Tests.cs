using Aoc2023Net.Tests.Attributes;
using NUnit.Framework;

namespace Aoc2023Net.Tests.Days
{
    [DayDataPart1(@".|...\....
                    |.-.\.....
                    .....|-...
                    ........|.
                    ..........
                    .........\
                    ..../.\\..
                    .-.-/..|..
                    .|....-|.\
                    ..//.|....", 46)]
    [DayDataPart1(7517)]
    [DayDataPart2(@".|...\....
                    |.-.\.....
                    .....|-...
                    ........|.
                    ..........
                    .........\
                    ..../.\\..
                    .-.-/..|..
                    .|....-|.\
                    ..//.|....", 51)]
    [DayDataPart2(7741)]
    [TestFixture]
    public sealed class Day16Tests : DayTests<Day16Tests>
    {
    }
}
