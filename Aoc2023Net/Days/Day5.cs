// If You Give A Seed A Fertilizer: https://adventofcode.com/2023/day/5

using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day5 : Day
    {
        private record Range(long SourceStart, long DestinationStart, long Length);

        private sealed class Map
        {
            private readonly Range[] _ranges;

            private readonly long _minSource;
            private readonly long _maxSource;
            private readonly long _minDestination;
            private readonly long _maxDestination;

            public Map(Range[] ranges)
            {
                _ranges = ranges;

                _minSource = _ranges.Min(r => r.SourceStart);
                _maxSource = _ranges.Max(r => r.SourceStart + r.Length - 1);

                _minDestination = _ranges.Min(r => r.DestinationStart);
                _maxDestination = _ranges.Max(r => r.DestinationStart + r.Length - 1);
            }

            public long GetDestination(long source)
            {
                if (source < _minSource || source > _maxSource)
                    return source;

                foreach (var r in _ranges)
                {
                    if (source < r.SourceStart || source >= r.SourceStart + r.Length)
                        continue;

                    return r.DestinationStart + (source - r.SourceStart);
                }

                return source;
            }

            public long GetSource(long destination)
            {
                if (destination < _minDestination || destination > _maxDestination)
                    return destination;

                foreach (var r in _ranges)
                {
                    if (destination < r.DestinationStart || destination >= r.DestinationStart + r.Length)
                        continue;

                    return r.SourceStart + (destination - r.DestinationStart);
                }

                return destination;
            }
        }

        public override object SolvePart1()
        {
            var (seeds, maps) = GetSeedsAndMaps();
            return seeds.Min(s => GetLocation(s, maps));
        }

        public override object SolvePart2()
        {
            var (seeds, maps) = GetSeedsAndMaps();
            var seedsRanges = seeds.Chunk(2).Select(c => (Start: c[0], Length: c[1])).ToArray();

            var heuristicSeedRangeSteps = 1000;
            var minLocation = seedsRanges
                .SelectMany(r => Enumerable
                    .Range(0, heuristicSeedRangeSteps)
                    .Select(i => r.Start + (r.Length / heuristicSeedRangeSteps) * i))
                .Select(s => GetLocation(s, maps))
                .Min();

            maps = maps.Reverse().ToArray();

            for (var location = minLocation - 1; location >= 0; location--)
            {
                var seed = maps.Aggregate(location, (r, m) => m.GetSource(r));
                if (seedsRanges.Any(r => r.Start <= seed && seed < r.Start + r.Length))
                    minLocation = location;
            }

            return minLocation;
        }

        private long GetLocation(long seed, Map[] maps) =>
            maps.Aggregate(seed, (result, m) => m.GetDestination(result));

        private (long[] Seeds, Map[] Maps) GetSeedsAndMaps()
        {
            var groups = InputData.GetInputLinesGroups();

            var seeds = Regex
                .Matches(groups[0].First(), @"\d+")
                .Select(m => long.Parse(m.Value))
                .ToArray();

            return (seeds, new[]
            {
                GetMap(groups[1]),
                GetMap(groups[2]),
                GetMap(groups[3]),
                GetMap(groups[4]),
                GetMap(groups[5]),
                GetMap(groups[6]),
                GetMap(groups[7])
            });
        }

        private Map GetMap(string[] rangesStrings) => new Map(rangesStrings
            .Skip(1)
            .Select(s => s.Split(' ').Select(long.Parse).ToArray())
            .Select(p => new Range(p[1], p[0], p[2]))
            .ToArray());
    }
}
