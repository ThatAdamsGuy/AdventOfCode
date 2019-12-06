using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2019_6
{
    class Program
    {
        static List<KeyValuePair<string, string>> inputs;
        static void Main(string[] args)
        {
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                inputs = new List<KeyValuePair<string, string>>();
                string line;
                while ((line = tx.ReadLine()) != null)
                {
                    string[] splitInput = line.Split(')');
                    inputs.Add(new KeyValuePair<string, string>(splitInput[0], splitInput[1]));
                }
            }

            int orbits = 0;
            int counter = 1;

            //foreach (var each in inputs)
            //{
            //    orbits += GetOrbits(each, 1);
            //    counter++;
            //}

            Console.WriteLine("TOTAL Orbits: " + orbits);
            Console.WriteLine("Orbits between YOU and SAN: " + JumpToSanta());
        }

        static int GetOrbits(KeyValuePair<string, string> input, int counter)
        {
            if (input.Key == "COM")
            {
                return counter;
            }

            var matchingRecord = inputs.FirstOrDefault(t => t.Value.Equals(input.Key));
            if (matchingRecord.Key == "COM")
            {
                counter++;
                return counter;
            } else
            {
                counter++;
                return GetOrbits(matchingRecord, counter);
            }
        }

        static int JumpToSanta()
        {
            Console.Write("Planets from me to COM: ");
            List<string> MeToCOM = GetPlanets("YOU", new List<string>());
            List<string> SanToCOM = GetPlanets("SAN", new List<string>());
            Console.WriteLine("Me to COM: " + MeToCOM.Count);
            Console.WriteLine("Santa to COM: " + SanToCOM.Count);
            MeToCOM.Except(SanToCOM).Concat(SanToCOM.Except(MeToCOM));

            Console.WriteLine("MY PLANETS: ");
            int counter = 0;
            foreach(string item in MeToCOM)
            {
                Console.WriteLine(counter + " - " + item);
                counter++;
            }
            Console.WriteLine("SANTA'S PLANETS: ");
            counter = 0;
            foreach(string item in SanToCOM)
            {
                Console.WriteLine(counter + " - " + item);
                counter++;
            }

            return MeToCOM.Count;
        }

        static List<string> GetPlanets(string input, List<string> results)
        {
            var matchingRecord = inputs.FirstOrDefault(t => t.Value.Equals(input));

            if (matchingRecord.Key == "COM")
            {
                results.Add(matchingRecord.Key);
                return results;
            }
            else
            {
                results.Add(matchingRecord.Key);
                return GetPlanets(matchingRecord.Key, results);
            }
        }
    }
}
