using System;
using System.Collections.Generic;
using BlazorConnect4.Model;

namespace BlazorConnect4.AIModels
{

    public abstract class AI
    {
        // Funktion för att beskriva 
        public abstract int SelectMove(Cell[,] grid);
    }


    public class RandomAI : AI
    {
        Random generator;

        public RandomAI()
        {
            generator = new Random();
        }

        public override int SelectMove(Cell[,] grid)
        {
            return generator.Next(7);
        }
    }
    public class QLearn : AI
    {

        public List<(int, int)> GetValidMoves(GameBoard board)
        {
            var ValidMoves = new List<(int, int)>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 5; j > 0; j--)
                {
                    if (board.Grid[i, j].Color == CellColor.Blank)
                    {
                        ValidMoves.Add((i, j));
                        Console.WriteLine(j);
                        break;
                    }
                }
            }
            return ValidMoves;
        }
        private void RedQLearning()
        {
            
        }

        private void WriteFile()
        {
    
        }

        public void ReadFile()
        {

        }

        public override int SelectMove(Cell[,] grid)
        {
            throw new NotImplementedException();
        }
    }
}
