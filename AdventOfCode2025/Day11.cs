using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2025
{
    internal class Day11
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day11input.txt");
            var nodes = new Dictionary<string, Node>();
            
            // Build the graph
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var nodeName = parts[0].Replace(":", "");
                
                // Create source node if it doesn't exist
                if (!nodes.ContainsKey(nodeName))
                {
                    nodes[nodeName] = new Node(nodeName);
                }
                
                // Create and connect destination nodes
                for (int i = 1; i < parts.Length; i++)
                {
                    var destName = parts[i];
                    
                    // Create destination node if it doesn't exist
                    if (!nodes.ContainsKey(destName))
                    {
                        nodes[destName] = new Node(destName);
                    }
                    
                    // Add one way connection
                    nodes[nodeName].Destinations.Add(nodes[destName]);
                }
            }

            // Part 1: Original BFS solution
            if (nodes.ContainsKey("you") && nodes.ContainsKey("out"))
            {
                int routeCount = CountRoutesBFS(nodes["you"], nodes["out"]);
                Console.WriteLine($"Part 1 - {routeCount}");
            }
            else
            {
                Console.WriteLine("'you' or 'out' node not found in graph");
            }

            // Part 2: New DFS solution for paths with required nodes
            if (nodes.ContainsKey("svr") && nodes.ContainsKey("out"))
            {
                var memo = new Dictionary<string, long>();
                var visited = new HashSet<string>();
                var routeCount = CountPathsDFS(nodes["svr"], nodes["out"], 
                    visited, new HashSet<string> { "dac", "fft" }, memo);
                Console.WriteLine($"Part 2 - {routeCount}");
            }
            else
            {
                Console.WriteLine("'svr' or 'out' node not found in graph");
            }
        }

        /// <summary>
        /// Original BFS implementation for Part 1 - counts all paths from start to end
        /// </summary>
        private static int CountRoutesBFS(Node start, Node end)
        {
            var queue = new Queue<(Node currentNode, HashSet<string> visitedPath)>();
            queue.Enqueue((start, new HashSet<string> { start.Name }));
            
            int totalRoutes = 0;

            while (queue.Count > 0)
            {
                var (current, visited) = queue.Dequeue();

                // Check each destination from current node
                foreach (var neighbor in current.Destinations)
                {
                    // If we found the target, count this route
                    if (neighbor.Name == end.Name)
                    {
                        totalRoutes++;
                        continue;
                    }

                    // If we haven't visited this node in current path, explore it
                    if (!visited.Contains(neighbor.Name))
                    {
                        var newVisited = new HashSet<string>(visited) { neighbor.Name };
                        queue.Enqueue((neighbor, newVisited));
                    }
                }
            }

            return totalRoutes;
        }

        /// <summary>
        /// Recursive DFS with memoization to count paths visiting required nodes
        /// </summary>
        private static long CountPathsDFS(Node current, Node target, HashSet<string> visited, 
            HashSet<string> requiredNodes, Dictionary<string, long> memo)
        {
            // Create state key for memoization based on current node and which required nodes we've visited
            var visitedRequired = requiredNodes.Where(visited.Contains).OrderBy(x => x);
            string stateKey = $"{current.Name}|{string.Join(",", visitedRequired)}";
            
            if (memo.ContainsKey(stateKey))
                return memo[stateKey];

            // If we reached the target
            if (current == target)
            {
                // Check if we visited all required nodes
                bool hasAllRequired = requiredNodes.All(visited.Contains);
                return hasAllRequired ? 1 : 0;
            }

            // Add current node to visited set
            visited.Add(current.Name);
            long totalPaths = 0;

            // Explore all unvisited neighbors
            foreach (var neighbor in current.Destinations)
            {
                if (!visited.Contains(neighbor.Name))
                {
                    totalPaths += CountPathsDFS(neighbor, target, visited, requiredNodes, memo);
                }
            }

            // Backtrack - remove current node from visited set
            visited.Remove(current.Name);

            // Cache the result
            memo[stateKey] = totalPaths;
            return totalPaths;
        }
    }

    class Node
    {
        public string Name { get; set; }
        public List<Node> Destinations { get; set; }
        
        public Node(string name) 
        {
            Name = name;
            Destinations = new List<Node>();
        }
    }
}
