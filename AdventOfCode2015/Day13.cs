using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day13
    {
        public class Node
        {
            public string Name { get; set; }
            public Dictionary<Node, int> Connections { get; set; }

            public Node(string name)
            {
                Name = name;
                Connections = new Dictionary<Node, int>();
            }
        }

        public static void Run()
        {
            var graph = BuildGraph();
            var permutations = GetPermutations(graph);
    
            Console.WriteLine($"Part 1 - {CalculateHappiness(permutations)}");

            var me = new Node("Me");
            foreach(var node in graph) {
                me.Connections.Add(node, 0);
                node.Connections.Add(me, 0);
            }
            graph.Add(me);
            permutations = GetPermutations(graph);

            Console.WriteLine($"Part 2 - {CalculateHappiness(permutations)}");
        }

        private static int CalculateHappiness(IEnumerable<List<Node>> permutations)
        {
            int maxHappiness = int.MinValue;

            foreach (var perm in permutations)
            {
                int currentHappiness = 0;
                for (int i = 0; i < perm.Count; i++)
                {
                    var leftNeighbor = perm[(i - 1 + perm.Count) % perm.Count];
                    var rightNeighbor = perm[(i + 1) % perm.Count];
                    currentHappiness += perm[i].Connections[leftNeighbor];
                    currentHappiness += perm[i].Connections[rightNeighbor];
                }

                if (currentHappiness > maxHappiness)
                {
                    maxHappiness = currentHappiness;
                }
            }
            return maxHappiness;
        }

        private static IEnumerable<List<Node>> GetPermutations(List<Node> nodes)
        {
            if (nodes.Count <= 1)
            {
                yield return new List<Node>(nodes);
                yield break;
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                var current = nodes[i];
                var remaining = new List<Node>(nodes);
                remaining.RemoveAt(i);

                foreach (var permutation in GetPermutations(remaining))
                {
                    var result = new List<Node> { current };
                    result.AddRange(permutation);
                    yield return result;
                }
            }
        }

        private static List<Node> BuildGraph()
        {
            var graph = new List<Node>();
            var input = System.IO.File.ReadAllLines("day13input.txt");
            foreach (var line in input)
            {
                var splitLine = line.Split(' ');
                var rootNode = graph.FirstOrDefault(x => x.Name == splitLine[0]);
                if (rootNode == null)
                {
                    rootNode = new Node(splitLine[0]);
                    graph.Add(rootNode);
                }

                var targetNode = graph.FirstOrDefault(x => x.Name == splitLine[10].TrimEnd('.'));
                if (targetNode == null)
                {
                    targetNode = new Node(splitLine[10].TrimEnd('.'));
                    graph.Add(targetNode);
                }

                var happinessValue = int.Parse(splitLine[3]);
                if (splitLine[2] == "lose")
                {
                    happinessValue = -happinessValue;
                }

                rootNode.Connections[targetNode] = happinessValue;
            }

            return graph;
        }
    }
}
