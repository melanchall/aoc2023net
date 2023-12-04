// Scratchcards: https://adventofcode.com/2023/day/4

using Aoc2023Net.Utilities;
using System.Text.RegularExpressions;

namespace Aoc2023Net.Days
{
    internal sealed class Day4 : Day
    {
        private record Scratchcard(int CardNumber, int[] WinningNumbers, int[] Numbers);

        public override object SolvePart1()
        {
            var scratchcards = GetScratchcards();
            return scratchcards
                .Select(GetMatchesCount)
                .Where(c => c != 0)
                .Select(c => Math.Pow(2, c - 1))
                .Sum();
        }

        public override object SolvePart2()
        {
            var scratchcards = GetScratchcards();
            var matchesCountsCache = new int?[scratchcards.Length];

            var scratchcardsQueue = new Queue<Scratchcard>(scratchcards);
            var totalCount = 0;

            while (scratchcardsQueue.Count != 0)
            {
                var scratchcard = scratchcardsQueue.Dequeue();
                totalCount++;

                var matchesCount = matchesCountsCache[scratchcard.CardNumber] != null ?
                    matchesCountsCache[scratchcard.CardNumber]!.Value :
                    (matchesCountsCache[scratchcard.CardNumber] = GetMatchesCount(scratchcard)).Value;
                
                if (matchesCount != 0)
                {
                    for (var i = 1; i <= matchesCount; i++)
                    {
                        scratchcardsQueue.Enqueue(scratchcards[scratchcard.CardNumber + i]);
                    }
                }
            }

            return totalCount;
        }

        private int GetMatchesCount(Scratchcard scratchcard) =>
            scratchcard.WinningNumbers.Intersect(scratchcard.Numbers).Count();

        private Scratchcard[] GetScratchcards()
        {
            var result = new List<Scratchcard>();
            var scratchcardRegex = new Regex(@"Card\s+(\d+):(\s+\d+)+\s+\|(\s+\d+)+");

            foreach (var line in InputData.GetInputLines())
            {
                var match = scratchcardRegex.Match(line);
                var cardNumber = match.GetInt32Group(1) - 1;
                var winningNumbers = match.GetGroupInt32Captures(2);
                var numbers = match.GetGroupInt32Captures(3);
                result.Add(new (cardNumber, winningNumbers, numbers));
            }

            return [.. result];
        }
    }
}
