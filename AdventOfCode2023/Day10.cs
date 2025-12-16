using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day10
    {
        // Direction vectors: North, East, South, West
        static int[] dr = { -1, 0, 1, 0 };
        static int[] dc = { 0, 1, 0, -1 };

        public static void Run()
        {
            char[][] grid = File.ReadAllLines("day10input.txt")
                                .Select(line => line.ToCharArray())
                                .ToArray();

            // Find the 'S' character coordinates
            int startRow = -1, startCol = -1;
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == 'S')
                    {
                        startRow = row;
                        startCol = col;
                        break;
                    }
                }
                if (startRow != -1) break;
            }

            // Find the loop and calculate distances
            var distances = FindLoop(grid, startRow, startCol);
            int maxDistance = distances.Values.Max();
            
            Console.WriteLine($"Part 1: {maxDistance}");
        }

        private static Dictionary<(int, int), int> FindLoop(char[][] grid, int startRow, int startCol)
        {
            var distances = new Dictionary<(int, int), int>();
            var queue = new Queue<(int row, int col, int dist)>();
            
            // Find valid starting directions from S
            for (int dir = 0; dir < 4; dir++)
            {
                int newRow = startRow + dr[dir];
                int newCol = startCol + dc[dir];
                
                if (IsValidPosition(newRow, newCol, grid) && 
                    CanConnect(grid, startRow, startCol, newRow, newCol, dir))
                {
                    queue.Enqueue((newRow, newCol, 1));
                    distances[(startRow, startCol)] = 0;
                }
            }

            // BFS to trace the loop
            while (queue.Count > 0)
            {
                var (currentRow, currentCol, currentDist) = queue.Dequeue();
                var key = (currentRow, currentCol);
                
                // If we've already visited this position with a shorter distance, skip
                if (distances.ContainsKey(key) && distances[key] <= currentDist)
                    continue;
                
                distances[key] = currentDist;
                
                // Find next positions
                var nextPositions = GetNextPositions(grid, currentRow, currentCol);
                foreach (var (nextRow, nextCol) in nextPositions)
                {
                    var nextKey = (nextRow, nextCol);
                    if (!distances.ContainsKey(nextKey) || distances[nextKey] > currentDist + 1)
                    {
                        queue.Enqueue((nextRow, nextCol, currentDist + 1));
                    }
                }
            }
            
            return distances;
        }

        private static List<(int, int)> GetNextPositions(char[][] grid, int row, int col)
        {
            var positions = new List<(int, int)>();
            char currentPipe = grid[row][col];
            
            // Define connections for each pipe type
            var connections = GetPipeConnections(currentPipe);
            
            foreach (int direction in connections)
            {
                int newRow = row + dr[direction];
                int newCol = col + dc[direction];
                
                if (IsValidPosition(newRow, newCol, grid) && 
                    CanConnect(grid, row, col, newRow, newCol, direction))
                {
                    positions.Add((newRow, newCol));
                }
            }
            
            return positions;
        }

        private static List<int> GetPipeConnections(char pipe)
        {
            // Return directions this pipe connects to
            // 0=North, 1=East, 2=South, 3=West
            switch (pipe)
            {
                case '|': return new List<int> { 0, 2 }; // North-South
                case '-': return new List<int> { 1, 3 }; // East-West  
                case 'L': return new List<int> { 0, 1 }; // North-East
                case 'J': return new List<int> { 0, 3 }; // North-West
                case '7': return new List<int> { 2, 3 }; // South-West
                case 'F': return new List<int> { 1, 2 }; // South-East
                case 'S': return new List<int> { 0, 1, 2, 3 }; // All directions for start
                default: return new List<int>();
            }
        }

        private static bool CanConnect(char[][] grid, int fromRow, int fromCol, int toRow, int toCol, int direction)
        {
            if (!IsValidPosition(toRow, toCol, grid))
                return false;
                
            char fromPipe = grid[fromRow][fromCol];
            char toPipe = grid[toRow][toCol];
            
            // Skip ground
            if (toPipe == '.')
                return false;
            
            // Get opposite direction
            int oppositeDir = (direction + 2) % 4;
            
            // Check if both pipes connect in the right directions
            var fromConnections = GetPipeConnections(fromPipe);
            var toConnections = GetPipeConnections(toPipe);
            
            return fromConnections.Contains(direction) && toConnections.Contains(oppositeDir);
        }

        private static bool IsValidPosition(int row, int col, char[][] grid)
        {
            return row >= 0 && row < grid.Length &&
                   col >= 0 && col < grid[0].Length;
        }
    }
}
