using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day04
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day04Input.txt");
            int sum = 0;
            foreach(var line in input)
            {
                var numbers = line.Split(':')[1].Split('|');
                var winningNumbers = numbers[0].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
                var cardNumbers = numbers[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();

                sum += (int)Math.Pow(2, (winningNumbers.Intersect(cardNumbers).Count() - 1));
            }
            Console.WriteLine("Part 1 - " + sum);

            Dictionary<int, int> cards = new Dictionary<int, int>();

            foreach (var line in input)
            {
                var key = int.Parse(line.Split(':')[0].Split(' ').Last());

                if (cards.ContainsKey(key))
                {
                    cards[key]++;
                }
                else
                {
                    cards.Add(key, 1);
                }

                var numbers = line.Split(':')[1].Split('|');
                var winningNumbers = numbers[0].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();
                var cardNumbers = numbers[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();

                int winnings = winningNumbers.Intersect(cardNumbers).Count();
                for (int j = 0; j < cards[key]; j++)
                {
                    for (int i = 1; i <= winnings; i++)
                    {
                        if (cards.ContainsKey(key + i))
                        {
                            cards[key + i]++;
                        }
                        else
                        {
                            cards.Add(key + i, 1);
                        }
                    }
                }
            }
            Console.WriteLine("Part 2 - " + cards.Sum(x => x.Value));
        }
    }
}
