using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace futureProsHW
{

    class Program
    {
        static void Main(string[] args)
        {
            //string input = "2, 3, 1, 2, 10, 5, 2, -1, 1, 1, 2, 0, 1, 0 ";
            //string input = "1, 2, 0, 0, 1, 2, 0";
            //string input = "1, 2, 0, 1, 3, 2, 0";

            UI.showMainScreen();
            
        }

    }
    class Experiment
    {
        List<Move> bestPath = new List<Move>();
        public List<Move> solveProblem(int[] array, int currentPosition, int steps, List<Move> moves)
        {
            //this if is triggered when finish is reached. It checks whether the path is more efficient than the current most efficient path
            if (currentPosition >= array.Length - 1)
            {  
                if(bestPath.Count == 0 || moves.Count < bestPath.Count)
                {
                    bestPath.Clear();
                    foreach (var move in moves)
                    {
                        bestPath.Add(move);
                    }
                    return moves;
                }
            }
            else
                for (int i = array[currentPosition]; i > 0; i--)
                {
                    Move move = PathfindHelper.checkOutOfBounds(steps,array, currentPosition, i);
                    moves = PathfindHelper.checkBacktrack(steps, moves, move);
                    solveProblem(array, currentPosition + i, steps + 1, moves);   
                }
            return bestPath;
        }
        public  void cleanBest()
        {
            bestPath.Clear();               //before solving next array, old path must be removed
        }
    }

    class Move
    {
        private int step, from, fromAt, to, toAt;
        public Move(int step, int from, int fromAt, int to, int toAt)
        {
            this.step = step;
            this.from = from;
            this.fromAt = fromAt;
            this.to = to;
            this.toAt = toAt;
        }

        public override string ToString()
        {
            return ("Step " + step + ": from " + from + " at index " + fromAt + ",  to " + to + " at index " + toAt);
        }
    }

    class PathfindHelper
    {        
        public static Move checkOutOfBounds(int steps, int[] array, int currentPosition, int i)
        {
            int to, toAt;
            if (currentPosition + i < array.Length)

            {
                to = array[currentPosition + i];
                toAt = currentPosition + i;                  
            }
            else
            {
                //prevents from going out of bounds
                to = array[array.Length - 1];
                toAt = array.Length - 1;
            }
            return new Move(steps, array[currentPosition], currentPosition, to, toAt);
        }
        public static List<Move> checkBacktrack(int steps, List<Move> moves, Move move)
        {
            if (moves.Count < steps)
            {
                moves.Add(move);
            }
            else
            {
                //if backtracking, remove old moves from list
                moves.RemoveRange(steps - 1, moves.Count - steps + 1);
                moves.Add(move);
            }
            return moves;
        }
    }
}
