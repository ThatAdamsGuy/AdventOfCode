using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day24
    {
        public static void Run()
        {
            var containers = File.ReadAllLines("day24input.txt").Select(x => int.Parse(x)).ToList();
            
            var part1Result = FindSmallestGroupWithLowestProduct(containers, 3);
            Console.WriteLine($"Part 1 - {part1Result}");
            
            var part2Result = FindSmallestGroupWithLowestProduct(containers, 4);
            Console.WriteLine($"Part 2 - {part2Result}");
        }

        private static long FindSmallestGroupWithLowestProduct(List<int> containers, int numberOfGroups = 3)
        {
            int lowestGroupCount = int.MaxValue;
            List<List<int>> lowestGroups = new List<List<int>>();

            var totalSum = containers.Sum();
            if (totalSum % numberOfGroups != 0) return -1; // Can't split into equal groups

            var targetSum = totalSum / numberOfGroups;

            for (int groupSize = 1; groupSize <= containers.Count / numberOfGroups; groupSize++)
            {
                var validGroups = new List<List<int>>();
                foreach (var combo in GetCombinationsOfSizeIterative(containers, groupSize))
                {
                    if (combo.Sum() == targetSum)
                    {
                        validGroups.Add(combo);
                    }
                }

                foreach (var firstGroup in validGroups)
                {
                    var remaining = containers.Except(firstGroup).ToList();

                    if (CanSplitIntoEqualGroups(remaining, targetSum, numberOfGroups - 1))
                    {
                        if (firstGroup.Count < lowestGroupCount)
                        {
                            lowestGroups.Clear();
                            lowestGroupCount = firstGroup.Count;
                            lowestGroups.Add(firstGroup);
                        }
                        else if (firstGroup.Count == lowestGroupCount)
                        {
                            lowestGroups.Add(firstGroup);
                        }
                    }
                }

                if (lowestGroups.Count > 0) break;
            }

            if (lowestGroups.Count == 0) return -1;

            var groupWithLowestProduct = lowestGroups
                .OrderBy(group => group.Aggregate(1L, (acc, x) => acc * x))
                .First();

            return groupWithLowestProduct.Aggregate(1L, (acc, x) => acc * x);
        }

        private static bool CanSplitIntoEqualGroups(List<int> items, int targetSum, int groupsRemaining)
        {
            if (groupsRemaining == 1)
            {
                return items.Sum() == targetSum;
            }
            
            if (groupsRemaining == 2)
            {
                return HasSubsetSum(items, targetSum);
            }
            
            foreach (var combo in GetCombinationsOfSizeIterative(items))
            {
                if (combo.Sum() == targetSum)
                {
                    var remaining = items.Except(combo).ToList();
                    if (CanSplitIntoEqualGroups(remaining, targetSum, groupsRemaining - 1))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private static IEnumerable<List<T>> GetCombinationsOfSizeIterative<T>(List<T> items, int? size = null)
        {
            if (!size.HasValue)
            {
                for (int s = 1; s <= items.Count; s++)
                {
                    foreach (var combo in GetCombinationsOfSizeIterative(items, s))
                    {
                        yield return combo;
                    }
                }
                yield break;
            }
            
            int actualSize = size.Value;
            
            if (actualSize == 0)
            {
                yield return new List<T>();
                yield break;
            }
            
            if (actualSize > items.Count)
                yield break;
                
            var indices = new int[actualSize];
            for (int i = 0; i < actualSize; i++)
                indices[i] = i;
            
            while (true)
            {
                var combination = new List<T>();
                for (int i = 0; i < actualSize; i++)
                {
                    combination.Add(items[indices[i]]);
                }
                yield return combination;
                
                int pos = actualSize - 1;
                while (pos >= 0 && indices[pos] == items.Count - actualSize + pos)
                    pos--;
                    
                if (pos < 0)
                    break;
                    
                indices[pos]++;
                for (int i = pos + 1; i < actualSize; i++)
                    indices[i] = indices[i - 1] + 1;
            }
        }

        private static bool HasSubsetSum(List<int> items, int targetSum)
        {
            int n = items.Count;
            int totalCombinations = 1 << n;

            for (int i = 1; i < totalCombinations; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        sum += items[j];
                    }
                }
                if (sum == targetSum)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
