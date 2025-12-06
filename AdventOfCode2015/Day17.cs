using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day17
    {
        public static void Run()
        {
            List<int> containers = File.ReadAllLines("Day17Input.txt").Select(int.Parse).ToList();
            int targetVolume = 150;
            List<List<int>> validCombinations = new List<List<int>>();
            int containerCount = containers.Count;
            int minContainersUsed = int.MaxValue;
            int minContainerCombinationCount = 0;
            for (int i = 1; i < (1 << containerCount); i++)
            {
                List<int> currentCombination = new List<int>();
                int currentVolume = 0;
                for (int j = 0; j < containerCount; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        currentCombination.Add(containers[j]);
                        currentVolume += containers[j];
                    }
                }
                if (currentVolume == targetVolume)
                {
                    validCombinations.Add(currentCombination);
                    if (currentCombination.Count <= minContainersUsed)
                    {
                        minContainersUsed = currentCombination.Count;
                        minContainerCombinationCount++;
                    }
                }
            }

            Console.WriteLine("Part 1 - " + validCombinations.Count);
            Console.WriteLine("Part 2 - " + minContainerCombinationCount);
        }
    }
}
