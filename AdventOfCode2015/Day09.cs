using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    static class Day09
    {
        [Serializable]
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
            var graph = CreateGraph();
            Console.WriteLine("Part 1 - " + FindShortestPath(graph));
            Console.WriteLine("Part 2 - " + FindLongestPath(graph));
        }


        private static int FindShortestPath(List<Node> graph)
        {
            var minDistance = int.MaxValue;

            // Try starting from each node
            foreach (var startNode in graph)
            {
                var visited = new HashSet<Node>();
                var distance = FindShortestPathRecursive(startNode, visited, 0, graph.Count);
                minDistance = Math.Min(minDistance, distance);
            }

            return minDistance;
        }

        private static int FindLongestPath(List<Node> graph)
        {
            var maxDistance = 0;

            // Try starting from each node
            foreach (var startNode in graph)
            {
                var visited = new HashSet<Node>();
                var distance = FindLongestPathRecursive(startNode, visited, 0, graph.Count);
                maxDistance = Math.Max(maxDistance, distance);
            }

            return maxDistance;
        }

        private static int FindLongestPathRecursive(Node currentNode, HashSet<Node> visited, int currentDistance, int totalNodes)
        {
            visited.Add(currentNode);

            // If we've visited all nodes, return the current distance
            if (visited.Count == totalNodes)
            {
                visited.Remove(currentNode);
                return currentDistance;
            }

            var maxDistance = 0;

            // Try visiting each unvisited connected node
            foreach (var connection in currentNode.Connections)
            {
                if (!visited.Contains(connection.Key))
                {
                    var distance = FindLongestPathRecursive(
                        connection.Key,
                        visited,
                        currentDistance + connection.Value,
                        totalNodes);
                    maxDistance = Math.Max(maxDistance, distance);
                }
            }

            visited.Remove(currentNode);
            return maxDistance;
        }

        private static int FindShortestPathRecursive(Node currentNode, HashSet<Node> visited, int currentDistance, int totalNodes)
        {
            visited.Add(currentNode);

            // If we've visited all nodes, return the current distance
            if (visited.Count == totalNodes)
            {
                visited.Remove(currentNode);
                return currentDistance;
            }

            var minDistance = int.MaxValue;

            // Try visiting each unvisited connected node
            foreach (var connection in currentNode.Connections)
            {
                if (!visited.Contains(connection.Key))
                {
                    var distance = FindShortestPathRecursive(
                        connection.Key,
                        visited,
                        currentDistance + connection.Value,
                        totalNodes);
                    minDistance = Math.Min(minDistance, distance);
                }
            }

            visited.Remove(currentNode);
            return minDistance;
        }

        private static List<Node> CreateGraph()
        {
            var lines = File.ReadAllLines("day09input.txt");
            var graph = new List<Node>();
            foreach (var line in lines)
            {
                var splitLine = line.Split(' ');
                Node rootNode;
                if (!graph.Any(x => x.Name == splitLine[0]))
                {
                    rootNode = new Node(splitLine[0]);
                    graph.Add(rootNode);
                }
                else
                {
                    rootNode = graph.Single(x => x.Name == splitLine[0]);
                }

                Node destNode;
                if (!graph.Any(x => x.Name == splitLine[2]))
                {
                    destNode = new Node(splitLine[2]);
                    graph.Add(destNode);
                }
                else
                {
                    destNode = graph.Single(x => x.Name == splitLine[2]);
                }

                rootNode.Connections.Add(destNode, int.Parse(splitLine[4]));
                destNode.Connections.Add(rootNode, int.Parse(splitLine[4]));
            }
            return graph;
        }


    }
}
