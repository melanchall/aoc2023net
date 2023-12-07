// Camel Cards: https://adventofcode.com/2023/day/7

namespace Aoc2023Net.Days
{
    internal sealed class Day7 : Day
    {
        private enum HandType
        {
            HighCard,
            Pair,
            TwoPair,
            Three,
            FullHouse,
            Four,
            Five,
        }

        private const string Alphabet = "23456789TJQKA";
        private const string JokerAlphabet = "J23456789TQKA";

        private sealed class HandBid
        {
            private static readonly Dictionary<HandType, Predicate<ICollection<IGrouping<char, char>>>> HandTypesRules = new()
            {
                [HandType.HighCard] = g => g.Count == 5,
                [HandType.Pair] = g => g.Count == 4,
                [HandType.TwoPair] = g => g.Count == 3 && g.Count(gg => gg.Count() == 2) == 2,
                [HandType.Three] = g => g.Count == 3 && g.Any(gg => gg.Count() == 3),
                [HandType.FullHouse] = g => g.Count == 2 && g.Any(gg => gg.Count() == 3),
                [HandType.Four] = g => g.Any(gg => gg.Count() == 4),
                [HandType.Five] = g => g.Count == 1
            };

            public HandBid(string hand, int bid)
            {
                Hand = hand;
                Bid = bid;
            }

            public string Hand { get; }

            public int Bid { get; }

            public HandType GetHandType(bool useJoker)
            {
                return useJoker
                    ? Alphabet.Max(c => GetHandTypeInternal(Hand.Replace('J', c)))
                    : GetHandTypeInternal(Hand);
            }

            private HandType GetHandTypeInternal(string hand)
            {
                var charGroups = hand.GroupBy(c => c).ToArray();
                return HandTypesRules.First(r => r.Value(charGroups)).Key;
            }
        }

        public override object SolvePart1() =>
            GetTotalWinnings(Alphabet, false);

        public override object SolvePart2() =>
            GetTotalWinnings(JokerAlphabet, true);

        private int GetTotalWinnings(string alphabet, bool useJoker) => GetHandsBids()
            .OrderBy(hb => hb.GetHandType(useJoker))
            .ThenBy(hb => alphabet.IndexOf(hb.Hand[0]))
            .ThenBy(hb => alphabet.IndexOf(hb.Hand[1]))
            .ThenBy(hb => alphabet.IndexOf(hb.Hand[2]))
            .ThenBy(hb => alphabet.IndexOf(hb.Hand[3]))
            .ThenBy(hb => alphabet.IndexOf(hb.Hand[4]))
            .Select((hb, i) => hb.Bid * (i + 1))
            .Sum();

        private HandBid[] GetHandsBids() => InputData
            .GetInputLines()
            .Select(l =>
            {
                var parts = l.Split(' ');
                return new HandBid(parts[0], int.Parse(parts[1]));
            })
            .ToArray();
    }
}
