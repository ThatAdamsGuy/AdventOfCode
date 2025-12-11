using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2025
{
    internal class Day10
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day10input.txt");
            var problems = new List<Problem>();
            
            foreach(var line in input)
            {
                var split = line.Split(' ').ToList();
                var start = split[0].Replace("[", "").Replace("]","");
                var end = split[split.Count-1];
                split.RemoveAt(0); // Remove pattern
                split.RemoveAt(split.Count-1); // Remove numbers in braces
                
                var buttons = new List<List<int>>();
                foreach(var button in split)
                {
                    var newButton = button.Replace("(", "").Replace(")", "");
                    if (!string.IsNullOrEmpty(newButton))
                    {
                        var ints = newButton.Split(',').Select(x => int.Parse(x.Trim())).ToList();
                        buttons.Add(ints);
                    }
                }
                problems.Add(new Problem(start, buttons));
            }
            
            Console.WriteLine($"Parsed {problems.Count} problems");
            
            int totalSteps = 0;
            int solvedCount = 0;
            
            foreach(var problem in problems)
            {
                int steps = SolveProblem(problem);
                if (steps != -1)
                {
                    totalSteps += steps;
                    solvedCount++;
                }
                else
                {
                    Console.WriteLine($"Could not solve problem with pattern: {problem.RequiredSolution}");
                }
            }
            
            Console.WriteLine($"Part 1: Total steps for all solvable problems: {totalSteps}");
            Console.WriteLine($"Solved {solvedCount} out of {problems.Count} problems");


            var machines = new List<Machine>();

            foreach (var line in input)
            {
                var split = line.Split(' ');
                var requiredLights = new List<int>();

                for (var i = split[0].IndexOf('#'); i > -1; i = split[0].IndexOf('#', i + 1))
                {
                    requiredLights.Add(i - 1);
                }

                var buttons = new List<int[]>();

                for (var i = 1; i < split.Length - 1; i++)
                {
                    // Replace [1..^1] with traditional string manipulation
                    var buttonStr = split[i];
                    var button = buttonStr.Substring(1, buttonStr.Length - 2).Split(',').Select(int.Parse).ToArray();
                    buttons.Add(button);
                }

                // Replace split[^1][1..^1] with traditional array indexing and substring
                var lastSplit = split[split.Length - 1];
                var joltages = lastSplit.Substring(1, lastSplit.Length - 2).Split(',').Select(int.Parse).ToArray();

                var machine = new Machine(requiredLights.ToArray(), buttons.ToArray(), joltages, new int[0]);
                machines.Add(machine);
            }

            var part2 = 0;

            for (var i = 0; i < machines.Count; i++)
            {
                var machine = machines[i];
                var joltages = machine.Joltages.Length;
                var buttons = machine.Buttons.Length;

                var matrix = new int[joltages, buttons + 1];

                for (var eq = 0; eq < joltages; eq++)
                {
                    for (var btn = 0; btn < buttons; btn++)
                    {
                        if (machine.Buttons[btn].Contains(eq))
                        {
                            matrix[eq, btn] = 1;
                        }
                    }
                    matrix[eq, buttons] = machine.Joltages[eq];
                }

                var solution = SolveGaussianElimination(matrix, buttons, joltages);

                if (solution != null)
                {
                    part2 += solution.Sum();
                    Console.WriteLine("Solved problem " + i);
                }
            }

            Console.WriteLine($"Part 2: {part2}");
        }
        
        private static int SolveProblem(Problem problem)
        {
            int arrayLength = problem.RequiredSolution.Length;
            
            // Convert target pattern to boolean array
            bool[] target = new bool[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                target[i] = problem.RequiredSolution[i] == '#';
            }
            
            // BFS to find minimum button presses
            var queue = new Queue<(bool[] state, int steps)>();
            var visited = new HashSet<string>();
            
            // Starting state: all zeros (false)
            bool[] initialState = new bool[arrayLength];
            queue.Enqueue((initialState, 0));
            visited.Add(StateToString(initialState));
            
            while (queue.Count > 0)
            {
                var (currentState, steps) = queue.Dequeue();
                
                // Check if we've reached the target
                if (StatesEqual(currentState, target))
                {
                    return steps;
                }
                
                // Try each button
                for (int buttonIndex = 0; buttonIndex < problem.Buttons.Count; buttonIndex++)
                {
                    var newState = (bool[])currentState.Clone();
                    
                    // Apply button effect (flip positions)
                    foreach (int position in problem.Buttons[buttonIndex])
                    {
                        if (position >= 0 && position < arrayLength)
                        {
                            newState[position] = !newState[position];
                        }
                    }
                    
                    string stateString = StateToString(newState);
                    if (!visited.Contains(stateString))
                    {
                        visited.Add(stateString);
                        queue.Enqueue((newState, steps + 1));
                    }
                }
            }
            
            return -1; // No solution found
        }
        
        private static bool StatesEqual(bool[] state1, bool[] state2)
        {
            if (state1.Length != state2.Length) return false;
            for (int i = 0; i < state1.Length; i++)
            {
                if (state1[i] != state2[i]) return false;
            }
            return true;
        }
        
        private static string StateToString(bool[] state)
        {
            var sb = new StringBuilder();
            foreach (bool b in state)
            {
                sb.Append(b ? '1' : '0');
            }
            return sb.ToString();
        }

        static int[] SolveGaussianElimination(int[,] matrix, int buttons, int joltages)
        {
            var pivotCols = new List<int>();
            var currentRow = 0;

            // Forward elimination
            for (var col = 0; col < buttons && currentRow < joltages; col++)
            {
                // Find pivot
                var pivotRow = -1;
                for (var row = currentRow; row < joltages; row++)
                {
                    if (matrix[row, col] != 0)
                    {
                        pivotRow = row;
                        break;
                    }
                }

                if (pivotRow == -1)
                {
                    continue;
                }

                // Swap rows
                if (pivotRow != currentRow)
                {
                    for (var j = 0; j <= buttons; j++)
                    {
                        (matrix[currentRow, j], matrix[pivotRow, j]) = (matrix[pivotRow, j], matrix[currentRow, j]);
                    }
                }

                pivotCols.Add(col);

                // Eliminate below
                for (var row = currentRow + 1; row < joltages; row++)
                {
                    if (matrix[row, col] != 0)
                    {
                        var factor = matrix[row, col];
                        var pivotVal = matrix[currentRow, col];

                        for (var j = col; j <= buttons; j++)
                        {
                            matrix[row, j] = matrix[row, j] * pivotVal - matrix[currentRow, j] * factor;
                        }

                        // Reduce by GCD to prevent coefficient explosion
                        var gcd = 0;
                        for (var j = col; j <= buttons; j++)
                        {
                            if (matrix[row, j] != 0)
                            {
                                gcd = gcd == 0 ? Math.Abs(matrix[row, j]) : GCD(gcd, Math.Abs(matrix[row, j]));
                            }
                        }
                        if (gcd > 1)
                        {
                            for (var j = col; j <= buttons; j++)
                            {
                                matrix[row, j] /= gcd;
                            }
                        }
                    }
                }

                currentRow++;
            }

            // Find free variables
            var pivotSet = new HashSet<int>(pivotCols);
            var freeVars = new List<int>();
            for (var i = 0; i < buttons; i++)
            {
                if (!pivotSet.Contains(i))
                {
                    freeVars.Add(i);
                }
            }

            // No free variables - unique solution or no solution
            if (freeVars.Count == 0)
            {
                return TrySolution(matrix, buttons, joltages, pivotCols, freeVars, new int[0]);
            }

            int[] bestSolution = null;
            var bestSum = int.MaxValue;

            // Compute search bounds based on free variable count
            int maxVal;
            switch (freeVars.Count)
            {
                case 1:
                    maxVal = Math.Max(matrix[0, buttons] * 3, 2000);
                    break;
                case 2:
                    maxVal = ComputeMaxVal(matrix, pivotCols);
                    break;
                case 3:
                    maxVal = 500;
                    break;
                case 4:
                    maxVal = 200;
                    break;
                case 5:
                    maxVal = 100;
                    break;
                default:
                    maxVal = 0;
                    break;
            }

            if (freeVars.Count > 5)
            {
                return TrySolution(matrix, buttons, joltages, pivotCols, freeVars, new int[freeVars.Count]);
            }

            var current = new int[freeVars.Count];
            SearchFreeVars(0);

            return bestSolution;

            void SearchFreeVars(int depth)
            {
                if (depth == freeVars.Count)
                {
                    var solution = TrySolution(matrix, buttons, joltages, pivotCols, freeVars, current);
                    if (solution != null)
                    {
                        var totalPresses = solution.Sum();
                        if (totalPresses < bestSum)
                        {
                            bestSum = totalPresses;
                            bestSolution = solution;
                        }
                    }
                    return;
                }

                var limit = freeVars.Count == 1 ? maxVal + 1 : maxVal;

                // Pruning: skip if current partial sum already exceeds best
                var partialSum = 0;
                for (var i = 0; i < depth; i++)
                {
                    partialSum += current[i];
                }

                for (var v = 0; v < limit; v++)
                {
                    // Early exit if partial sum exceeds best
                    if (partialSum + v >= bestSum)
                        break;

                    current[depth] = v;
                    SearchFreeVars(depth + 1);
                }
            }
        }

        static int ComputeMaxVal(int[,] matrixParam, List<int> pivotColsParam)
        {
            var maxValLocal = 0;
            var buttonsLocal = matrixParam.GetLength(1) - 1;
            for (var row = 0; row < pivotColsParam.Count; row++)
            {
                maxValLocal = Math.Max(maxValLocal, Math.Abs(matrixParam[row, buttonsLocal]));
            }
            return Math.Min(Math.Max(maxValLocal * 2, 500), 2000);
        }

        static int[] TrySolution(int[,] matrix, int buttons, int joltages, List<int> pivotCols, List<int> freeVars, int[] freeValues)
        {
            var solution = new int[buttons];

            // Set free variables
            for (var i = 0; i < freeVars.Count; i++)
            {
                solution[freeVars[i]] = freeValues[i];
            }

            // Back substitution for pivot variables
            for (var i = pivotCols.Count - 1; i >= 0; i--)
            {
                var row = i;
                var col = pivotCols[i];
                var total = matrix[row, buttons];

                for (var j = col + 1; j < buttons; j++)
                {
                    total -= matrix[row, j] * solution[j];
                }

                if (matrix[row, col] == 0 || total % matrix[row, col] != 0)
                    return null;

                var val = total / matrix[row, col];
                if (val < 0) return null;

                solution[col] = val;
            }

            // Verify solution
            for (var i = 0; i < joltages; i++)
            {
                var sum = 0;
                for (var j = 0; j < buttons; j++)
                {
                    sum += matrix[i, j] * solution[j];
                }
                if (sum != matrix[i, buttons]) return null;
            }

            return solution;
        }

        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public struct Machine
        {
            public int[] RequiredLights { get; }
            public int[][] Buttons { get; }
            public int[] Joltages { get; }
            public int[] Lights { get; }

            public Machine(int[] requiredLights, int[][] buttons, int[] joltages, int[] lights)
            {
                RequiredLights = requiredLights;
                Buttons = buttons;
                Joltages = joltages;
                Lights = lights;
            }
        }
    }

    class Problem
    {
        public string RequiredSolution { get; set; }
        public List<List<int>> Buttons { get; set; }
        
        public Problem(string requiredSolution, List<List<int>> buttons)
        {
            RequiredSolution = requiredSolution;
            Buttons = buttons;
        }
    }
}
