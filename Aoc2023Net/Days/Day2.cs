// Cube Conundrum: https://adventofcode.com/2023/day/2

using Aoc2023Net.Utilities;
using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day2 : Day
    {
        private record Set(int RedCount, int GreenCount, int BlueCount);
        private record Game(int GameId, Set[] Sets);

        public override object SolvePart1() => GetGames()
            .Where(g => g.Sets.All(s => s.RedCount <= 12 && s.GreenCount <= 13 && s.BlueCount <= 14))
            .Sum(g => g.GameId);

        public override object SolvePart2() => GetGames()
            .Select(g => g.Sets.Max(s => s.RedCount) * g.Sets.Max(s => s.GreenCount) * g.Sets.Max(s => s.BlueCount))
            .Sum();

        private Game[] GetGames()
        {
            var result = new List<Game>();

            var gameRegex = new Regex(@"Game\s+(\d+):\s+(.+)");
            var colorCountRegex = new Regex(@"(\d+)\s+(.+)");

            foreach (var line in InputData.GetInputLines())
            {
                var match = gameRegex.Match(line);
                var gameId = match.GetInt32Group(1);

                var setsGroup = match.GetStringGroup(2);
                var setsStrings = setsGroup.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var sets = new List<Set>();

                foreach (var setString in setsStrings)
                {
                    var cubesCounts = setString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    var redCount = 0;
                    var greenCount = 0;
                    var blueCount = 0;

                    foreach (var cubeCount in cubesCounts)
                    {
                        var colorCountMatch = colorCountRegex.Match(cubeCount);
                        var count = colorCountMatch.GetInt32Group(1);
                        var color = colorCountMatch.GetStringGroup(2);

                        if (color == "red")
                            redCount += count;
                        else if (color == "green")
                            greenCount += count;
                        else
                            blueCount += count;
                    }

                    sets.Add(new (redCount, greenCount, blueCount));
                }

                result.Add(new (gameId, sets.ToArray()));
            }

            return result.ToArray();
        }
    }
}
