using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day07
    {
        // Part 1: J is normal Jack
        private static readonly List<char> CardsPart1 = new List<char>() 
        { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'};
        
        // Part 2: J is weakest (Joker)
        private static readonly List<char> CardsPart2 = new List<char>() 
        { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'};

        public static void Run()
        {
            var input = File.ReadAllLines("day07input.txt");
            
            // Part 1
            Console.WriteLine("Part 1 - " + RunPart(input, false));
            
            // Part 2
            Console.WriteLine("Part 2 - " + RunPart(input, true));
        }

        private static long RunPart(string[] input, bool isPart2)
        {
            List<Combo> hands = new List<Combo>();
            foreach (var line in input)
            {
                var split = line.Split(' ');
                var handType = isPart2 ? CalculateTypePart2(split[0]) : CalculateTypePart1(split[0]);
                var combo = new Combo(split[0], int.Parse(split[1]), handType);
                hands.Add(combo);
            }

            // Order hands
            var orderedHands = hands.OrderBy(h => (int)h.Type)
                                   .ThenBy(h => h, new HandComparer(isPart2))
                                   .ToList();

            // Calculate total winnings
            long totalWinnings = 0;
            for (int i = 0; i < orderedHands.Count; i++)
            {
                int rank = i + 1;
                totalWinnings += orderedHands[i].Bid * rank;
            }

            return totalWinnings;
        }

        // Custom comparer class
        private class HandComparer : IComparer<Combo>
        {
            private readonly bool isPart2;

            public HandComparer(bool isPart2)
            {
                this.isPart2 = isPart2;
            }

            public int Compare(Combo x, Combo y)
            {
                if (x == null || y == null) return 0;
                if (x.Hand == y.Hand) return 0;

                if (RankHands(x, y, isPart2)) return 1;
                if (RankHands(y, x, isPart2)) return -1;
                return 0;
            }
        }

        private static bool RankHands(Combo handA, Combo handB, bool isPart2)
        {
            var cards = isPart2 ? CardsPart2 : CardsPart1;
            
            for(int i = 0; i < 5; i++)
            {
                if (handA.Hand[i] == handB.Hand[i]) continue;
                return cards.IndexOf(handA.Hand[i]) < cards.IndexOf(handB.Hand[i]);
            }
            return false; // Identical hands
        }

        // Part 1: Normal hand type calculation
        private static HandType CalculateTypePart1(string hand)
        {
            var groups = hand.GroupBy(c => c)
                             .Select(g => g.Count())
                             .OrderByDescending(count => count)
                             .ToList();

            if (groups.Count == 1) return HandType.Five;
            if (groups.Count == 2 && groups[0] == 4) return HandType.Four;
            if (groups.Count == 2 && groups[0] == 3) return HandType.FullHouse;
            if (groups.Count == 3 && groups[0] == 3) return HandType.Three;
            if (groups.Count == 3 && groups[0] == 2) return HandType.TwoPair;
            if (groups.Count == 4) return HandType.OnePair;
            return HandType.High;
        }

        // Part 2: J acts as wildcard for best hand type
        private static HandType CalculateTypePart2(string hand)
        {
            int jokerCount = hand.Count(c => c == 'J');
            
            // If no jokers, same as Part 1
            if (jokerCount == 0)
                return CalculateTypePart1(hand);
            
            // If all jokers, it's five of a kind
            if (jokerCount == 5)
                return HandType.Five;

            // Get counts of non-joker cards
            var nonJokerGroups = hand.Where(c => c != 'J')
                                    .GroupBy(c => c)
                                    .Select(g => g.Count())
                                    .OrderByDescending(count => count)
                                    .ToList();

            // Add jokers to the largest group
            nonJokerGroups[0] += jokerCount;

            // Determine hand type based on the modified groups
            if (nonJokerGroups[0] == 5) return HandType.Five;
            if (nonJokerGroups[0] == 4) return HandType.Four;
            if (nonJokerGroups[0] == 3 && nonJokerGroups.Count > 1 && nonJokerGroups[1] == 2) return HandType.FullHouse;
            if (nonJokerGroups[0] == 3) return HandType.Three;
            if (nonJokerGroups[0] == 2 && nonJokerGroups.Count > 1 && nonJokerGroups[1] == 2) return HandType.TwoPair;
            if (nonJokerGroups[0] == 2) return HandType.OnePair;
            return HandType.High;
        }

        enum HandType
        {
            High,
            OnePair,
            TwoPair,
            Three,
            FullHouse,
            Four,
            Five
        }

        class Combo
        {
            public string Hand { get; set; }
            public int Bid { get; set; }
            public HandType Type { get; set; }
            
            public Combo(string hand, int bid, HandType type)
            {
                Hand = hand;
                Bid = bid;
                Type = type;
            }
        }
    }
}
