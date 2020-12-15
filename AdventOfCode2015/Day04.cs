using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day04
    {
        public static void Run()
        {
            Queue<string> inputs = new Queue<string>(File.ReadAllLines("day04Known.txt"));
            Dictionary<string, int> wires = new Dictionary<string, int>();
            while (inputs.Count() > 0)
            {
                string line = inputs.Dequeue();
                List<string> curString = line.Split(new string[] { " -> " }, StringSplitOptions.None).ToList();
                if (int.TryParse(curString[0], out int posValue))
                {
                    //Value to Wire
                    wires.Add(curString[1], posValue);
                } else
                {
                    if (curString[0].Contains("AND"))
                    {
                        string[] postSplit = curString[0].Split(new string[] { " AND " }, StringSplitOptions.None);
                        if (wires.ContainsKey(postSplit[0]) && wires.ContainsKey(postSplit[1]))
                        {
                            int value = wires[postSplit[0]] & wires[postSplit[1]];
                            if (wires.ContainsKey(curString[1]))
                            {
                                wires[curString[1]] = value;
                            } else
                            {
                                wires.Add(curString[1], value);
                            }
                        } else
                        {
                            inputs.Enqueue(line);
                        }
                    }
                    else if (curString[0].Contains("OR"))
                    {
                        string[] postSplit = curString[0].Split(new string[] { " OR " }, StringSplitOptions.None);
                        if (wires.ContainsKey(postSplit[0]) && wires.ContainsKey(postSplit[1]))
                        {
                            int value = wires[postSplit[0]] | wires[postSplit[1]];
                            if (wires.ContainsKey(curString[1]))
                            {
                                wires[curString[1]] = value;
                            }
                            else
                            {
                                wires.Add(curString[1], value);
                            }
                        }
                        else
                        {
                            inputs.Enqueue(line);
                        }
                    }
                    else if (curString[0].Contains("LSHIFT"))
                    {
                        string[] postSplit = curString[0].Split(new string[] { " LSHIFT " }, StringSplitOptions.None);
                        if (wires.ContainsKey(postSplit[0]))
                        {
                            wires[postSplit[0]] = wires[postSplit[0]] << int.Parse(postSplit[1]);
                        } else
                        {
                            inputs.Enqueue(line);
                        }
                    }
                    else if (curString[0].Contains("RSHIFT"))
                    {
                        string[] postSplit = curString[0].Split(new string[] { " RSHIFT " }, StringSplitOptions.None);
                        if (wires.ContainsKey(postSplit[0]))
                        {
                            wires[postSplit[0]] = wires[postSplit[0]] >> int.Parse(postSplit[1]);
                        }
                        else
                        {
                            inputs.Enqueue(line);
                        }
                    }
                    else if (curString[0].Contains("NOT"))
                    {
                        if(wires.ContainsKey(curString[0].Replace("NOT ", "")))
                        {
                            wires[curString[1]] = ~wires[curString[0].Replace("NOT ", "")];
                        } else
                        {
                            inputs.Enqueue(line);
                        }
                    }
                    else
                    {
                        if (wires.ContainsKey(curString[0]))
                        {
                            wires[curString[1]] = wires[curString[0]];
                        } else
                        {
                            Console.WriteLine("ELSE");
                            inputs.Enqueue(line);
                        }
                    }
                }
            }
            foreach(var wire in wires.OrderBy(s => s.Key))
            {
                Console.WriteLine($"{wire.Key}: {wire.Value}");
            }
        }
    }
}
