using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day08
    {
        public static void Run()
        {
            long counter = 0;
            var inputs = File.ReadAllLines("Day08Input.txt");
            foreach(var item in inputs)
            {
                var outputs = item.Split('|')[1].Split(' ');
                foreach (var output in outputs)
                {
                    if (output.Length == 2 || output.Length == 3 || output.Length == 4 || output.Length == 7)
                        counter++;
                }
            }
            Console.WriteLine("Part 1 - " + counter);

            counter = 0;
            string number = "";
            foreach(var item in inputs)
            {
                var toCheck = item.Split('|')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var allDisplays = new List<string>(toCheck);
                allDisplays.AddRange(item.Split('|')[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList());

                if(toCheck.Count() != 4)
                {
                    throw new Exception();
                }

                string one = allDisplays.Where(s => s.Length == 2).FirstOrDefault();
                string four = allDisplays.Where(s => s.Length == 4).FirstOrDefault();
                string eight = allDisplays.Where(s => s.Length == 7).FirstOrDefault();

                if (one is null || four is null)
                {
                    throw new Exception();
                }


                foreach(var checking in toCheck)
                {
                    switch (checking.Length)
                    {
                        case 2:
                            number += "1";
                            Console.Write("1");
                            break;
                        case 3:
                            number += "7";
                            Console.Write("7");
                            break;
                        case 4:
                            number += "4";
                            Console.Write("4");
                            break;
                        case 7:
                            number += "8";
                            Console.Write("8");
                            break;
                        case 6:
                            string sixMask = four;
                            foreach (char c in one)
                            {
                                if (sixMask.Contains(c))
                                {
                                    sixMask = sixMask.Replace(c.ToString(), String.Empty);
                                }
                            }
                            string tmpMask = eight;
                            foreach(char c in sixMask)
                            {
                                if (tmpMask.Contains(c))
                                {
                                    tmpMask = tmpMask.Replace(c.ToString(), String.Empty);
                                }
                            }

                            bool valid = true;
                            foreach (char c in tmpMask)
                            {
                                if (!checking.Contains(c))
                                    valid = false;
                            }
                            if (valid)
                            {
                                number += "0";
                                Console.Write("0");
                                break;
                            }

                            valid = true;
                            foreach(char c in one)
                            {
                                if (!checking.Contains(c))
                                    valid = false;
                            }
                            if (valid)
                            {
                                number += "9";
                                Console.Write("9");
                                break;
                            } else
                            {
                                number += "6";
                                Console.Write("6");
                                break;
                            }

                            

                        case 5:
                            valid = true;
                            foreach(char c in one)
                            {
                                if(!checking.Contains(c))
                                    valid = false;
                            }
                            if (valid)
                            {
                                number += "3";
                                Console.Write("3");
                                break;
                            }

                            if(four is null)
                            {
                                Console.WriteLine("Work to do, sorry.");
                                break;
                            }

                            //Get Four without One mask
                            string mask = four;
                            foreach (char c in one)
                            {
                                if (mask.Contains(c)) {
                                    mask = mask.Replace(c.ToString(), String.Empty);
                                }
                            }

                            valid = true;
                            foreach (char c in mask)
                            {
                                if (!checking.Contains(c))
                                {
                                    valid = false;
                                    break;
                                }
                            }
                            if (valid)
                            {
                                number += "5";
                                Console.Write("5");
                                break;
                            }

                            number += "2";
                            Console.Write("2");
                            break;
                        default:
                            Console.WriteLine("Something went wrong.");
                            break;

                    }
                }
                Console.WriteLine();
                if(number.Length != 4)
                {
                    throw new Exception();
                }
                counter += int.Parse(number);
                number = "";
            }
            Console.WriteLine("Part 2 - " + counter);
        }
    }
}
