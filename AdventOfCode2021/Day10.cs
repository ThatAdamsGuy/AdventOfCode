using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day10
    {
        public static void Run()
        {
            List<string> input = File.ReadAllLines("Day10Input.txt").ToList();
            List<string> p2Input = new List<string>(input);

            int errorCount = 0;
            foreach(var line in input)
            {
                Stack<char> opens = new Stack<char>();
                foreach (char c in line)
                {
                    bool valid = true;
                    if(c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        opens.Push(c);
                        continue;
                    }
                    switch (c)
                    {
                        case ')':
                            if(opens.Pop() != '(')
                            {
                                valid = false;
                                errorCount += 3;
                            }
                            break;
                        case ']':
                            if (opens.Pop() != '[')
                            {
                                valid = false;
                                errorCount += 57;
                            }
                            break;
                        case '}':
                            if (opens.Pop() != '{')
                            {
                                valid = false;
                                errorCount += 1197;
                            }
                            break;
                        case '>':
                            if (opens.Pop() != '<')
                            {
                                valid = false;
                                errorCount += 25137;
                            }
                            break;
                    }
                    if (!valid)
                    {
                        p2Input.Remove(line);
                        break;
                    }
                }
            }

            Console.WriteLine("Part 1 - " + errorCount);

            List<char> openers = new List<char>() { '(', '[', '{', '<' };
            List<char> closers = new List<char>() { ')', ']', '}', '>' };

            List<long> scores = new List<long>();

            foreach (var item in p2Input)
            {
                long score = 0;
                Stack<char> opens = new Stack<char>();
                foreach (char c in item)
                {
                    if (openers.Contains(c))
                    {
                        opens.Push(c);
                    } else
                    {
                        _ = opens.Pop();
                    }
                }

                foreach(var open in opens)
                {
                    switch (open)
                    {
                        case '(':
                            score = (score * 5) + 1;
                            break;
                        case '[':
                            score = (score * 5) + 2;
                            break;
                        case '{':
                            score = (score * 5) + 3;
                            break;
                        case '<':
                            score = (score * 5) + 4;
                            break;
                    }
                }
                scores.Add(score);
            }

            scores.Sort();
            Console.WriteLine("Part 2 - " + scores[scores.Count() / 2]);
        }
    }
}
