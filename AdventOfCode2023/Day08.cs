using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day08
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day08input.txt").ToList();
            string directions = input[0];
            input.RemoveAt(0);
            input.RemoveAt(0);
            var graph = GenerateGraph(input);

            Console.WriteLine("Part 1 - " + GetStepsPart1(graph, directions));
            Console.WriteLine("Part 2 - " + GetStepsPart2(graph, directions));
        }

        private static long GetStepsPart1(List<Node> graph, string directions)
        {
            var currentNode = graph.Where(x => x.Title == "AAA").Single();
            long steps = 0;

            while (currentNode.Title != "ZZZ")
            {
                foreach (char direction in directions)
                {
                    currentNode = direction == 'L' ? currentNode.Left : currentNode.Right;
                    steps++;

                    if (currentNode.Title == "ZZZ")
                        break;
                }
            }
            return steps;
        }

        private static long GetStepsPart2(List<Node> graph, string directions)
        {
            var startingNodes = graph.Where(x => x.Title.EndsWith("A")).ToList();
            var cycleLengths = new List<long>();

            // Find cycle length for each starting node
            foreach (var startNode in startingNodes)
            {
                var currentNode = startNode;
                long steps = 0;

                while (!currentNode.Title.EndsWith("Z"))
                {
                    foreach (char direction in directions)
                    {
                        currentNode = direction == 'L' ? currentNode.Left : currentNode.Right;
                        steps++;

                        if (currentNode.Title.EndsWith("Z"))
                            break;
                    }
                }
                cycleLengths.Add(steps);
            }

            // Calculate LCM of all cycle lengths
            return cycleLengths.Aggregate(LCM);
        }

        private static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LCM(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        private static List<Node> GenerateGraph(List<string> input)
        {
            var graph = new List<Node>();
            var nodeDict = new Dictionary<string, Node>();

            // First pass: create all nodes
            foreach (var line in input)
            {
                var split = line.Split(new string[] { " = " }, StringSplitOptions.None);
                var nodeTitle = split[0];
                var connections = split[1].Replace("(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.None);

                if (!nodeDict.ContainsKey(nodeTitle))
                {
                    nodeDict[nodeTitle] = new Node(nodeTitle);
                }

                if (!nodeDict.ContainsKey(connections[0]))
                {
                    nodeDict[connections[0]] = new Node(connections[0]);
                }

                if (!nodeDict.ContainsKey(connections[1]))
                {
                    nodeDict[connections[1]] = new Node(connections[1]);
                }
            }

            // Second pass: establish connections
            foreach (var line in input)
            {
                var split = line.Split(new string[] { " = " }, StringSplitOptions.None);
                var nodeTitle = split[0];
                var connections = split[1].Replace("(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.None);

                var node = nodeDict[nodeTitle];
                node.Left = nodeDict[connections[0]];
                node.Right = nodeDict[connections[1]];
            }

            return nodeDict.Values.ToList();
        }

        class Node
        {
            public string Title { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(string title)
            {
                Title = title;
            }
        }
    }
}