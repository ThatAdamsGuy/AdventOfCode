using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day06
    {
        /*
         * I forgot that a list based solution would not scale. Part 1 is my poor solution before the second part eventually solved this.
         */

        public static void Run()
        {
            //Part 1
            int days = 256;
            int fishCount = 0;
            List<Fish> workingFish = new List<Fish>();
            List<Fish> newFish = new List<Fish>();

            var startingFish = File.ReadAllLines("Day06Input.txt").First().Split(',');
            foreach(var fish in startingFish)
            {
                newFish.Add(new Fish(0)
                {
                    DaysRemaining = int.Parse(fish)
                });
            }

            int counter = 0;
            while(newFish.Count != 0)
            {
                workingFish = new List<Fish>(newFish);
                newFish.Clear();
                foreach(var fish in workingFish)
                {
                    for(int i = fish.BirthDay; i < days; i++)
                    {
                        if(fish.DaysRemaining == 0)
                        {
                            fish.DaysRemaining = 6;
                            newFish.Add(new Fish(i + 1));
                        } else
                        {
                            fish.DaysRemaining--;
                        }
                    }
                }
                fishCount += workingFish.Count();
                counter++;
                Console.WriteLine($"Loops Complete - {counter}. Processed {workingFish.Count()}");
            }
            Console.WriteLine($"Part 1 - {fishCount}");



            //Part 2

            //Array position is days until birth, array value is number of fish on that day
            long[] daysToBirth = new long[9];

            foreach(var input in startingFish)
            {
                daysToBirth[long.Parse(input)]++;
            }

            for(int i = 0; i < days; i++)
            {
                long born = daysToBirth[0];

                for(int k = 0; k < daysToBirth.Length - 1; k++)
                {
                    //Remove a day remaining
                    daysToBirth[k] = daysToBirth[k + 1];
                }

                //Add new fish to the pool (ba dum tsh)
                //Fish already born, go back to 6
                daysToBirth[6] += born;
                //Add new fish
                daysToBirth[8] = born;
            }

            long count = 0;
            foreach(long item in daysToBirth)
            {
                count += item;
            }
            Console.WriteLine($"Part 2 - {count}");
        }
    }

    internal class Fish
    {
        public int BirthDay;
        public int DaysRemaining;

        public Fish(int birthDay)
        {
            BirthDay = birthDay;
            DaysRemaining = 8;
        }
    }
}
