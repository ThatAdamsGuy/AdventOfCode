using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day04
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day04Input.txt").ToList();
            string[] bingoCalls = input.First().Split(',');

            //Known input, remove non-board items
            input.RemoveAt(0);
            input.RemoveAt(0);

            List<BingoBoard> boards = new List<BingoBoard>();
            List<string> board = new List<string>();
            foreach(var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    boards.Add(new BingoBoard(board.ToArray()));
                    board = new List<string>();
                } else
                {
                    board.Add(line);
                }
            }
            if (board.Count() != 0)
            {
                boards.Add(new BingoBoard(board.ToArray()));
            }


            BingoBoard winningBoard = null;
            BingoBoard losingBoard = null;
            List<BingoBoard> losingBoardList = boards;
            foreach (var call in bingoCalls)
            {
                List<BingoBoard> remainingBoards = new List<BingoBoard>(losingBoardList);
                int newCall = int.Parse(call);
                foreach(var tmpBoard in remainingBoards)
                {
                    tmpBoard.UpdateBoard(newCall);
                    if(tmpBoard.CheckRows() || tmpBoard.CheckColumns())
                    {
                        if(winningBoard == null)
                        {
                            Console.WriteLine($"Part 1 - {tmpBoard.GetUnmarkedSum() * newCall}");
                            winningBoard = tmpBoard;
                        } else if (losingBoardList.Count() == 1)
                        {
                            losingBoard = tmpBoard;
                        }
                        losingBoardList.Remove(tmpBoard);
                    }
                }
                if(losingBoard!= null)
                {
                    Console.WriteLine($"Part 2 - {losingBoard.GetUnmarkedSum() * newCall}");
                    break;
                }
            }
        }
    }

    class BingoBoard
    {
        public int[,] Board { get; set; }
        public BingoBoard(string[] board)
        {
            Board = new int[5,5];
            for (int i = 0; i < board.Length; i++)
            {
                string[] line = board[i].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
                for (int k = 0; k < line.Length; k++)
                {
                    Board[i, k] = int.Parse(line[k]);
                }
            }
        }

        public int GetUnmarkedSum()
        {
            int sum = 0;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] != -1)
                    {
                        sum += Board[i, j];
                    }
                }
            }
            return sum;
        }

        public void UpdateBoard(int bingoValue)
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if(Board[i, j] == bingoValue)
                    {
                        Board[i, j] = -1;
                    }
                }
            }
        }

        public bool CheckRows()
        {
            for(int i = 0; i < Board.GetLength(0); i++)
            {
                bool clearedRow = true;
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if(Board[i,j] != -1)
                    {
                        clearedRow = false;
                        break;
                    }
                }
                if (clearedRow)
                    return true;
            }
            return false;
        }

        public bool CheckColumns()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                bool clearedColumn = true;
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[j, i] != -1)
                    {
                        clearedColumn = false;
                        break;
                    }
                }
                if (clearedColumn)
                    return true;
            }
            return false;
        }
    }
}
