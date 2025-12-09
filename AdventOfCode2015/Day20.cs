using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day20
    {
        public static void Run()
        {
            int targetPresents = 29000000;

            Console.WriteLine($"Part 1: {FindHouseWithPresentsOptimized(targetPresents, part1: true)}");
            Console.WriteLine($"Part 2: {FindHouseWithPresentsOptimized(targetPresents, part1: false)}");
        }

        static int FindHouseWithPresentsOptimized(int target, bool part1)
        {
            // Pre-calculate upper bound for search
            int maxHouse = part1 ? target / 10 : target / 11;
            
            // Use array to accumulate presents for each house
            int[] presents = new int[maxHouse + 1];

            if (part1)
            {
                // Part 1: Each elf delivers to all multiples of their number
                for (int elf = 1; elf <= maxHouse; elf++)
                {
                    for (int house = elf; house <= maxHouse; house += elf)
                    {
                        presents[house] += elf * 10;
                    }
                }
            }
            else
            {
                // Part 2: Each elf delivers to first 50 houses that are multiples
                for (int elf = 1; elf <= maxHouse; elf++)
                {
                    int deliveryCount = 0;
                    for (int house = elf; house <= maxHouse && deliveryCount < 50; house += elf)
                    {
                        presents[house] += elf * 11;
                        deliveryCount++;
                    }
                }
            }

            // Find first house with enough presents
            for (int house = 1; house <= maxHouse; house++)
            {
                if (presents[house] >= target)
                {
                    return house;
                }
            }

            return -1; // Not found
        }

        // Keep optimized divisor method as backup for verification
        static int CalculatePresentsOptimized(int houseNumber, bool part1)
        {
            int presents = 0;
            int multiplier = part1 ? 10 : 11;

            // Only check up to sqrt(houseNumber) for efficiency
            for (int elf = 1; elf * elf <= houseNumber; elf++)
            {
                if (houseNumber % elf == 0)
                {
                    // Add presents from elf
                    if (part1 || houseNumber / elf <= 50)
                    {
                        presents += elf * multiplier;
                    }

                    // Add presents from paired divisor (if different)
                    if (elf * elf != houseNumber)
                    {
                        int pairedElf = houseNumber / elf;
                        if (part1 || houseNumber / pairedElf <= 50)
                        {
                            presents += pairedElf * multiplier;
                        }
                    }
                }
            }

            return presents;
        }

        // Original method for comparison (don't use - too slow!)
        static int FindHouseWithPresents(int target, bool part1)
        {
            int houseNumber = 1;

            while (true)
            {
                int presents = CalculatePresentsOptimized(houseNumber, part1);

                if (presents >= target)
                {
                    return houseNumber;
                }

                houseNumber++;
                
                // Progress indicator for large searches
                if (houseNumber % 10000 == 0)
                {
                    Console.WriteLine($"Checking house {houseNumber}...");
                }
            }
        }
    }
}
