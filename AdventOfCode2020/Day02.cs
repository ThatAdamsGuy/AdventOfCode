using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day02
    {
        public static void Run()
        {
            List<string> inputs = File.ReadAllLines("day02Input.txt").ToList();
            int validOldPasswords = 0;
            int validNewPasswords = 0;
            foreach(string password in inputs)
            {
                var splitPassword = password.Split(' ');
                string toParse = splitPassword[2];
                var limits = splitPassword[0].Split('-');
                int lowerLimit = int.Parse(limits[0]);
                int higherLimit = int.Parse(limits[1]);
                var requiredChar = splitPassword[1][0];

                int occurrences = 0;
                foreach(char each in splitPassword[2])
                {
                    if (each == requiredChar) occurrences++;
                }
                if (occurrences >= lowerLimit && occurrences <= higherLimit) validOldPasswords++;

                if((toParse[lowerLimit-1] == requiredChar && toParse[higherLimit-1] != requiredChar) 
                    || (toParse[lowerLimit-1] != requiredChar && toParse[higherLimit-1] == requiredChar))
                {
                    validNewPasswords++;
                }
                    
            }
            Console.WriteLine($"Valid old passwords: {validOldPasswords}");
            Console.WriteLine($"Valid new passwords: {validNewPasswords}");
        }
    }
}
