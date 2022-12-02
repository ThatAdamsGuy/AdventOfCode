using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day02
    {
        private enum Play
        {
            Rock,
            Paper,
            Scissors
        }

        private enum Result
        {
            Win,
            Lose,
            Draw
        }

        public static void Run()
        {
            int scoreSum = 0;
            foreach (string line in File.ReadLines("Day02Input.txt"))
            {
                var split = line.Split(' ');
                scoreSum += (GetWinningScore(GetPlay(split[0]), GetPlay(split[1])) + GetSelectionScore(GetPlay(split[1])));
            }
            Console.WriteLine("Part 1: " + scoreSum);

            scoreSum = 0;
            foreach (string line in File.ReadLines("Day02Input.txt"))
            {
                var split = line.Split(' ');
                scoreSum += GetResultAndPlayScore(GetPlay(split[0]), GetResult(split[1]));
            }
            Console.WriteLine("Part 2: " + scoreSum);
        }

        private static Play GetPlay(string input)
        {
            switch (input)
            {
                case "A":
                case "X": return Play.Rock;
                case "B":
                case "Y": return Play.Paper;
                case "C":
                case "Z": return Play.Scissors;
                default: throw new ArgumentException();
            }
        }

        private static Result GetResult(string input)
        {
            switch (input)
            {
                case "X": return Result.Lose;
                case "Y": return Result.Draw;
                case "Z": return Result.Win;
                default: throw new ArgumentException();
            }
        }

        private static int GetWinningScore(Play opponent, Play you)
        {
            if (opponent == you) return 3;
            if (opponent == Play.Rock && you == Play.Paper) return 6;
            if (opponent == Play.Paper && you == Play.Scissors) return 6;
            if (opponent == Play.Scissors && you == Play.Rock) return 6;
            return 0;
        }

        private static int GetResultAndPlayScore(Play opponent, Result result)
        {
            int selectionScore = GetSelectionScore(GetDesiredOutcome(opponent, result));
            switch (result)
            {
                case Result.Win:
                    return 6 + selectionScore;
                case Result.Draw:
                    return 3 + selectionScore;
                case Result.Lose:
                    return selectionScore;
                default:
                    throw new ArgumentException();
            }
        }

        private static Play GetDesiredOutcome(Play play, Result result)
        {
            if (result == Result.Draw) return play;

            else if (result == Result.Win)
            {
                switch (play)
                {
                    case Play.Rock: return Play.Paper;
                    case Play.Paper: return Play.Scissors;
                    case Play.Scissors: return Play.Rock;
                    default: throw new ArgumentException();
                }
            }
            else if (result == Result.Lose)
            {
                switch (play)
                {
                    case Play.Rock: return Play.Scissors;
                    case Play.Paper: return Play.Rock;
                    case Play.Scissors: return Play.Paper;
                    default: throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static int GetSelectionScore(Play play)
        {
            switch (play)
            {
                case Play.Rock: return 1;
                case Play.Paper: return 2;
                case Play.Scissors: return 3;
                default: return 0;
            }
        }
    }
}
