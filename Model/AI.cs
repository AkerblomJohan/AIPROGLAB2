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

        private double learningRate;
        private double discount;
        private int[][] qTable;
        private double Qvalue;
        private double epsilon = 0;
        double reward = 0;
        Random randomGen;
        private QLearn()
        {
            randomGen = new Random();
        }
       
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
        public int[] GetValidMoveArray(Cell[,] board)
        {
            List<int> validAction = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                if (board[i, 0].Color == CellColor.Blank)
                {
                    validAction.Add(i);
                }
            }
            return validAction.ToArray();
        }
        private double GetReward()
        {
            
            return this.reward;
        }

        private int[] getAction(int choice)
        {
            if(randomGen.NextDouble() < epsilon)
            {
                
            }
            return null;
        }
        
        private void RedQLearning(Cell[,] board)
        {
            
            var reward = 0; //reward for taking an action in a state
            var maxQ = 0; //max expedted future reward
            Qvalue = Qvalue + learningRate * (reward + discount * maxQ - Qvalue);

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
