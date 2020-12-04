using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day04
    {
        public static void Run()
        {
            List<string> inputs = File.ReadAllLines("day04Input.txt").ToList();
            int validPassports = 0;
            int validatedPassports = 0;

            Dictionary<string, string> parts = new Dictionary<string, string>();
            foreach(string line in inputs)
            {
                var splitLine = line.Split(' ');
                foreach(var item in splitLine)
                {
                    if (string.IsNullOrWhiteSpace(item)) break;
                    parts.Add(item.Split(':')[0], item.Split(':')[1]);
                }

                if (string.IsNullOrWhiteSpace(line) || line.Equals(inputs.Last()))
                {
                    var result = IsValidPassport(parts);
                    validPassports += (result.Item1);
                    validatedPassports += (result.Item2);
                    parts.Clear();
                    continue;
                }
            }
            Console.WriteLine($"Valid passports:     {validPassports}");
            Console.WriteLine($"Validated passports: {validatedPassports}");
        }

        public static Tuple<int,int> IsValidPassport(Dictionary<string,string> inputs)
        {
            List<string> validEyeColours = new List<string>()
            {
                "amb",
                "blu",
                "brn",
                "gry",
                "grn",
                "hzl",
                "oth"
            };

            if (inputs.ContainsKey("byr")
                && inputs.ContainsKey("iyr")
                && inputs.ContainsKey("eyr")
                && inputs.ContainsKey("hgt")
                && inputs.ContainsKey("hcl")
                && inputs.ContainsKey("ecl")
                && inputs.ContainsKey("pid"))
            {
                int birthyear = int.Parse(inputs["byr"]);
                if (birthyear < 1920 || birthyear > 2002) return Tuple.Create(1, 0);

                int issueyear = int.Parse(inputs["iyr"]);
                if (issueyear < 2010 || issueyear > 2020) return Tuple.Create(1, 0);

                int expyear = int.Parse(inputs["eyr"]);
                if (expyear < 2020 || expyear > 2030) return Tuple.Create(1, 0);

                string height = inputs["hgt"];
                string heightUnit = height.Substring(height.Length - 2, 2);
                if(heightUnit.Equals("cm"))
                {
                    int heightValue = int.Parse(height.Substring(0, height.Length - 2));
                    if (heightValue < 150 || heightValue > 193) return Tuple.Create(1, 0);
                }
                else if (heightUnit.Equals("in"))
                {
                    int heightValue = int.Parse(height.Substring(0, height.Length - 2));
                    if (heightValue < 59 || heightValue > 76) return Tuple.Create(1, 0);
                }               
                else
                {
                    return Tuple.Create(1, 0);
                }

                string haircolour = inputs["hcl"];
                Regex r = new Regex(@"\A\b[0-9a-f]+\b\Z");
                if (haircolour.Length != 7 || haircolour[0] != '#' || !r.IsMatch(haircolour.Substring(1, 6))) return Tuple.Create(1, 0);

                if (!validEyeColours.Contains(inputs["ecl"])) return Tuple.Create(1, 0);

                if (inputs["pid"].Length != 9 && !long.TryParse(inputs["pid"], out _)) return Tuple.Create(1, 0);

                return Tuple.Create(1, 1);

            } else
            {
                return Tuple.Create(0,0);
            }
        }
    }
}
