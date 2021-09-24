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




    public class QAgent : AI
    {
        public Dictionary<int, int> Qdict;
        int reward = 0;
        public double Rewards(int state, int action)
        {
            return 0;
        }


        // Return en probability för en given state och action där ju större probability desto bättre move är det 
        public double BestMoveProb(int state, int action)
        {
            var Qdict = new Dictionary<int, int>();
            Qdict.Add(state, action);
            if(Qdict == null)
            {
                Qdict[(state, action).state] = 1;
            }
            return Qdict;
        }






        public override int SelectMove(Cell[,] grid)
        {
            throw new NotImplementedException();
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
        public Random rnd {get ; set;}


       
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

        //create Q matrix, all values start at 0
        static double[][] CreateQMatrix(int size)
        {
            double[][] Q = new double[size][];
            for (int i = 0; i < size; ++i)
			{
                Q[i] = new double[size];
			}
            return Q;
        }
        //borde returnera alla tomma rutor (onödig kan använda GetValidMoveArray)
        static List<int> GetPossNextState(int s, Cell[,] FT)
        {
            List<int> Result = new List<int>();
            for (int i = 0; i < FT.Length; ++i)
			{
                if(FT[s,i].Color == CellColor.Blank) Result.Add(i);
			}
            return Result;
        }
        //
        static List<int> GetRandSate(int s,Cell[,] FT)
        {
            //väljer et slumpat sate
             List<int> possNextStates = GetPossNextState(s, FT);
             int ct = possNextStates.Count;
             int idx = rnd.Next(0, ct);
             return possNextStates[idx];
        }

        //q learning
        static void Train(Cell[,] FT, double[][] R, double[][] Q, int goial, double gamma, double learnRate, int MaxEpock)
        {
            for (int epoch = 0; epoch < MaxEpock; ++epoch)//går nog gör x antal gånger ba
			{
                int currState = rnd.Next(0,R.Length);

                while(true)
                {
                    int nextState = GetRandSate(currState,FT);
                    List<int> possNextuppState = GetPossNextState(nextState,FT);
                    double maxQ = double.MinValue;
                    for (int i = 0; i < possNextuppState.Count; i++)
			        {
                        int nns = possNextuppState[i];
                        double q = Q[nextState][nns];
                        if(q>maxQ)
                            maxQ = q;
			        }
                    Q[currState,nextState] = ((1-learnRate)*Q[currState,nextState])+(learnRate*(R[currState,nextState]+(gamma * maxQ)));
                    currState = nextState;
                    if (currState == goial) 
                        break;
                }
			}
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









