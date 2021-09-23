using System;
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
        private void RedQLearning()
        {

        }

        private void WriteFile()
        {
    
        }

        private void ReadFile()
        {

        }

        public override int SelectMove(Cell[,] grid)
        {
            throw new NotImplementedException();
        }
    }
}
