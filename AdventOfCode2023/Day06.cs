using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day06
    {
        public static void Run()
        {
            List<Race> races = new List<Race> {
                new Race(46, 214),
                new Race(80, 1177),
                new Race(78, 1402),
                new Race(66, 1024),
            };
            /*
            List<Race> races = new List<Race>
            {
                new Race(7, 9),
                new Race(15, 40),
                new Race(30, 200),
            };
            */
            List<int> ints = new List<int>();
            foreach (var race in races)
            {
                ints.Add(CalculateRaces(race));
            }
            Console.WriteLine("Part 1 - " + ints.Aggregate((x, y) => x * y));
            Console.WriteLine("Part 2 - " + CalculateRaces(new Race(46807866, 214117714021024)));
        }

        private static int CalculateRaces(Race race)
        {
            int count = 0;
            for (int i = 1; i < race.Time; i++)
            {
                var waitTime = i;
                var runTime = race.Time - i;

                count += race.Distance - (runTime * waitTime) < 0 ? 1: 0;
            }
            return count;
        }

        class Race
        {
            public long Time { get; set; }
            public long Distance { get; set; }

            public Race(long time, long distance) {
                Time = time;
                Distance = distance;
            }
        }
    }
}
