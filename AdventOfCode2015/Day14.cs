using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day14
    {
        public static void Run()
        {
            List<Reindeer> reindeers = new List<Reindeer>();

            var lines = File.ReadAllLines("Day14Known.txt");
            foreach(var line in lines)
            {
                var newLine = line.Split(' ');
                reindeers.Add(new Reindeer()
                {
                    Name = newLine[0],
                    Speed = int.Parse(newLine[3]),
                    RunningTime = int.Parse(newLine[6]),
                    RestingTime = int.Parse(newLine[13]),
                });
            }

            int seconds = 2503;

            for(int i = 1; i <= seconds; i++)
            {
                foreach(var reindeer in reindeers)
                {
                    reindeer.CurrentActivitySeconds++;
                    if (reindeer.IsRunning)
                    {
                        reindeer.Distance += reindeer.Speed;
                        if(reindeer.CurrentActivitySeconds == i)
                        {
                            reindeer.IsRunning = false;
                            reindeer.CurrentActivitySeconds = 0;
                        }
                    } else
                    {
                        if(reindeer.CurrentActivitySeconds == reindeer.RestingTime)
                        {
                            reindeer.IsRunning = true;
                            reindeer.CurrentActivitySeconds = 0;
                        }
                    }
                }
            }

            int distance = 0;
            foreach(var reindeer in reindeers)
            {
                if (reindeer.Distance > distance)
                {
                    distance = reindeer.Distance;
                }
                Console.WriteLine($"{reindeer.Name} - {reindeer.Distance}km");
            }

            Console.WriteLine("Part 1 - " + distance);
        }
    }

    class Reindeer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int RunningTime { get; set; }
        public int RestingTime { get; set; }
        public bool IsRunning { get; set; }

        public int CurrentActivitySeconds { get; set; }
        public int Distance { get; set; }

        public Reindeer()
        {
            IsRunning = true;
            CurrentActivitySeconds = 0;
            Distance = 0;
        }
    }
}
