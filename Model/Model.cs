using System;
using System.Collections.Generic;
using BlazorConnect4.AIModels;

namespace BlazorConnect4.Model
{
    public enum CellColor
    {
        Red,
        Yellow,
        Blank
    }


    public class Cell
    {
        public CellColor Color {get; set;}

        public Cell(CellColor color)
        {
            Color = color;
        }

    }

    public class GameBoard : IEquatable<GameBoard>
    {
        public Cell[,] Grid { get; set; }

        public GameBoard()
        {
            Grid = new Cell[7, 6];

            //Populate the Board with blank pieces
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    Grid[i, j] = new Cell(CellColor.Blank);
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GameBoard);
        }

        public bool Equals(GameBoard other)
        {
            return other != null &&
                   EqualityComparer<Cell[,]>.Default.Equals(Grid, other.Grid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Grid);
        }
    }


    public class GameEngine
    {
        public GameBoard Board { get; set; }
        public CellColor Player { get; set;}
        public bool active;
        public String message;
        private AI ai;


        public GameEngine()
        {
            Reset("Human");
        }



        // Reset the game and creats the opponent.
        // TODO change the code so new RL agents are created.
        public void Reset(String playAgainst)
        {
            Board = new GameBoard();
            Player = CellColor.Red;
            active = true;
            message = "Starting new game";

            if (playAgainst == "Human")
            {
                ai = null;
            }
            else if (playAgainst == "Random")
            {
                ai = new RandomAI();
            }
            else if (playAgainst == "Q1")
            {
                ai = new RandomAI();
            }
            else if (playAgainst == "Q2")
            {
                ai = new RandomAI();
            }
            else if (playAgainst == "Q3")
            {
                ai = new RandomAI();
            }

        }


        private bool IsValid(int col)
        {
            return Board.Grid[col, 0].Color == CellColor.Blank;
        }


        public bool IsDraw()
        {
            for (int i = 0; i < 7; i++)
            {
                if (Board.Grid[i,0].Color == CellColor.Blank)
                {
                    return false;
                }
            }
            return true;
        }


        public bool IsWin(int col, int row)
        {
            bool win = false;
            int score = 0;
            

            // Check down
            if (row < 3)
            {
                for (int i = row; i <= row + 3; i++)
                {
                    if (Board.Grid[col,i].Color == Player)
                    {
                        score++;
                    }
                }
                win = score == 4;
                score = 0;
            }

            // Check horizontal

            int left = Math.Max(col - 3, 0);

            for (int i = left; i <= col; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i+j <= 6 && Board.Grid[i+j,row].Color == Player)
                    {
                        score++;
                    }
                }
                win = win || score == 4;
                score = 0;
            }

            // Check left down diagonal

            int colpos;
            int rowpos;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    colpos = col - i + j;
                    rowpos = row - i + j;
                    if (0 <= colpos && colpos <= 6 &&
                        0 <= rowpos && rowpos < 6 &&
                        Board.Grid[colpos,rowpos].Color == Player)
                    {
                        score++;
                    }
                }

                win = win || score == 4;
                score = 0;
            }

            // Check left up diagonal

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    colpos = col - i + j;
                    rowpos = row - i - j;
                    if (0 <= colpos && colpos <= 6 &&
                        0 <= rowpos && rowpos < 6 &&
                        Board.Grid[colpos, rowpos].Color == Player)
                    {
                        score++;
                    }
                }

                win = win || score == 4;
                score = 0;
            }

            return win;
        }




        public bool Play(int col)
        {
            if (IsValid(col) && active){

                for (int i = 5; i >= 0; i--)
                {
                    if (Board.Grid[col, i].Color == CellColor.Blank)
                    {
                        Board.Grid[col, i].Color = Player;
                        
                        if (IsWin(col,i))
                        {
                            if (Board.Grid[col, i].Color == CellColor.Red)
                            {
                                //AI +1
                            }
                            else
                                // AI -1
                            message = Player.ToString() + " Wins";
                            active = false;
                            
                            return true;
                        }

                        if (IsDraw())
                        {
                            if (Board.Grid[col, i].Color == CellColor.Red)
                            {
                                //AI -0,5
                            }
                            message = "Draw";
                            active = false;
                            return true;
                        }
                        break;
                    }
                }
                return PlayNext();
            }
            
            return false;
        }




        private bool PlayNext()
        {

            if (Player == CellColor.Red)
            {
                Player = CellColor.Yellow;
            }
            else
            {
                Player = CellColor.Red;
            }

            if (ai != null && Player == CellColor.Yellow)
            {
                int move = ai.SelectMove(Board.Grid);

                while (! IsValid(move))
                {
                    move = ai.SelectMove(Board.Grid);
                }

                return Play(move);
            }

            return false;
        }
    }


}
