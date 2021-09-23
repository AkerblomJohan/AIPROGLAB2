using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        
        public List<Tuple<int,int>> GetValidMoves(Cell[,] board)
        {

            var ValidMoves = new List<Tuple<int, int>>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 5; j >= 0; j--)
                {
                    if (board[i, j].Color == CellColor.Blank)
                    {
                        
                        ValidMoves.Add(new Tuple<int,int>(i,j));
                        
                        break;
                    }
                }
            }
            return ValidMoves;
        }

        Random random;
        private byte[,] TakeAction(Cell[,] board)
        {

            var validAction = GetValidMoves(board);
            int randomIndex = random.Next(0, validAction.Count);
            Tuple<int,int> move = validAction[randomIndex];



            return [0, 0];
        }
        private void RedQLearning()
        {
            double Discount = 0.5; //0 < x < 1

        }

        public void WriteFile()
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
