using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day12
    {
        public static void Run()
        {
            List<Cave> caves = new List<Cave>();
            var inputs = File.ReadAllLines("Day12Input.txt");
            foreach(var item in inputs)
            {
                var split = item.Split('-');
                Cave startingCave = caves.Any(s => s.Name.Equals(split[0])) ? caves.Where(s => s.Name.Equals(split[0])).Single() : new Cave(split[0]);
                if (!caves.Contains(startingCave)) caves.Add(startingCave);

                Cave finishingCave = caves.Any(s => s.Name.Equals(split[1])) ? caves.Where(s => s.Name.Equals(split[1])).Single() : new Cave(split[1]);
                if (!caves.Contains(finishingCave)) caves.Add(finishingCave);
                startingCave.LinkedCaves.Add(finishingCave);
                finishingCave.LinkedCaves.Add(startingCave);
            }

            Console.WriteLine("Part 1 - " + ExploreCavesPt1(caves.Where(s => s.Name.Equals("start")).Single(), new List<Cave>()));
            Console.WriteLine("Part 2 - " + ExploreCavesPt2(caves.Where(s => s.Name.Equals("start")).Single(), new List<Cave>()));
        }

        public static int ExploreCavesPt1(Cave startingCave, List<Cave> route)
        {
            List<Cave> newRoute = new List<Cave>(route);
            newRoute.Add(startingCave);
            int sum = 0;
            foreach(var cave in startingCave.LinkedCaves)
            {
                if (cave != startingCave) {
                    if (route.Contains(cave) && !cave.IsLarge)
                    {
                        continue;
                    }
                    if (cave.Name.Equals("end"))
                    {
                        sum++;
                    } else
                    {
                        sum += ExploreCavesPt1(cave, newRoute);
                    }
                }
            }
            return sum;
        }

        public static int ExploreCavesPt2(Cave startingCave, List<Cave> route)
        {
            List<Cave> newRoute = new List<Cave>(route);
            newRoute.Add(startingCave);

            int sum = 0;
            foreach (var cave in startingCave.LinkedCaves)
            {
                if (cave != startingCave)
                {
                    if (cave.Name.Equals("start"))
                    {
                        //Can't reenter the start.
                        continue;
                    }
                    if (!cave.IsLarge)
                    {
                        bool twoVisits = false;
                        foreach(var visitedCave in newRoute)
                        {
                            if(newRoute.Where(s => !s.IsLarge && s == visitedCave).Count() == 2)
                            {
                                //Already visited a small cave twice.
                                twoVisits = true;
                                break;
                            }
                        }
                        if(twoVisits && newRoute.Contains(cave)) continue;
                    }
                    if (cave.Name.Equals("end"))
                    {
                        sum++;
                    }
                    else
                    {
                        sum += ExploreCavesPt2(cave, newRoute);
                    }
                }
            }
            return sum;
        }
    }

    public class Cave
    {
        public string Name;
        public bool IsLarge;
        public List<Cave> LinkedCaves;

        public Cave(string name)
        {
            LinkedCaves = new List<Cave>();
            Name = name;
            IsLarge = name.All(c => char.IsUpper(c)); 
        }
    }
}
