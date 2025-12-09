using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day19
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day19Input.txt");
            var replacements = new List<Tuple<string, string>>();
            string targetMolecule = "";

            // Parse input: replacement rules + target molecule
            foreach (var line in input)
            {
                if (line.Contains(" => "))
                {
                    var parts = line.Split(new string[] { " => " }, StringSplitOptions.None);
                    replacements.Add(Tuple.Create(parts[0], parts[1]));
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    targetMolecule = line;
                }
            }

            Console.WriteLine($"Part 1: {CountDistinctMolecules(targetMolecule, replacements)}");
            Console.WriteLine($"Part 2: {FindMinSteps(targetMolecule, replacements)}");
        }

        static int CountDistinctMolecules(string molecule, List<Tuple<string, string>> replacements)
        {
            var distinctMolecules = new HashSet<string>();

            foreach (var replacement in replacements)
            {
                var from = replacement.Item1;
                var to = replacement.Item2;
                
                // Find all occurrences of 'from' in molecule and replace each one
                for (int i = 0; i <= molecule.Length - from.Length; i++)
                {
                    if (molecule.Substring(i, from.Length) == from)
                    {
                        string newMolecule = molecule.Substring(0, i) + to + molecule.Substring(i + from.Length);
                        distinctMolecules.Add(newMolecule);
                    }
                }
            }

            return distinctMolecules.Count;
        }

        static int FindMinSteps(string target, List<Tuple<string, string>> replacements)
        {
            // For AoC 2015 Day 19 Part 2, there's a mathematical insight:
            // The grammar has a specific structure that allows us to calculate the result directly
            
            // Try greedy approach first (works for most AoC inputs due to problem structure)
            int steps = FindMinStepsGreedy(target, replacements);
            if (steps != -1) return steps;
            
            // If greedy fails, fall back to BFS with better limits
            return FindMinStepsBFS(target, replacements);
        }

        static int FindMinStepsGreedy(string target, List<Tuple<string, string>> replacements)
        {
            string current = target;
            int steps = 0;
            
            // Sort replacements by length of replacement (longest first)
            // This prioritizes reductions that make the biggest impact
            var sortedReplacements = replacements
                .OrderByDescending(r => r.Item2.Length - r.Item1.Length)
                .ToList();
            
            while (current != "e")
            {
                string previous = current;
                
                // Try each replacement (working backwards: replace "to" with "from")
                foreach (var replacement in sortedReplacements)
                {
                    var from = replacement.Item1;
                    var to = replacement.Item2;
                    
                    // Keep applying this replacement as much as possible
                    while (current.Contains(to))
                    {
                        int index = current.IndexOf(to);
                        current = current.Substring(0, index) + from + current.Substring(index + to.Length);
                        steps++;
                    }
                }
                
                // If no progress was made, greedy approach failed
                if (current == previous)
                {
                    return -1;
                }
                
                // Safety check to prevent infinite loops
                if (steps > 1000)
                {
                    return -1;
                }
            }
            
            return steps;
        }

        static int FindMinStepsBFS(string target, List<Tuple<string, string>> replacements)
        {
            // Enhanced BFS with better pruning and limits
            var queue = new Queue<Tuple<string, int>>();
            var visited = new Dictionary<string, int>(); // Track best steps for each molecule
            
            queue.Enqueue(Tuple.Create(target, 0));
            visited[target] = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var molecule = current.Item1;
                var steps = current.Item2;

                // Check if we've reached "e"
                if (molecule == "e")
                {
                    return steps;
                }

                // Skip if we've found a better path to this molecule
                if (visited.ContainsKey(molecule) && visited[molecule] < steps)
                {
                    continue;
                }

                // Try reverse replacements
                foreach (var replacement in replacements)
                {
                    var from = replacement.Item1;
                    var to = replacement.Item2;
                    
                    // Replace "to" with "from" (working backwards)
                    int index = 0;
                    while ((index = molecule.IndexOf(to, index)) != -1)
                    {
                        string newMolecule = molecule.Substring(0, index) + from + molecule.Substring(index + to.Length);
                        int newSteps = steps + 1;
                        
                        // Only proceed if we haven't seen this molecule or found a better path
                        if (!visited.ContainsKey(newMolecule) || visited[newMolecule] > newSteps)
                        {
                            visited[newMolecule] = newSteps;
                            
                            // Increased limits for better coverage
                            if (newSteps <= 100 && visited.Count <= 500000)
                            {
                                queue.Enqueue(Tuple.Create(newMolecule, newSteps));
                            }
                        }
                        
                        index += to.Length;
                    }
                }
            }

            return -1; // No solution found
        }
    }
}
