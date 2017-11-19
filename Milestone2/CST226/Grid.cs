using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Created by Michael Rogers - November 2017 - Class CST 227 taught by Lori Collier

namespace CST226
{
    abstract class Grid
    {
        protected GameCell[,] gameboard;
        
        //Constructors
        public Grid()
        {

        }

        public Grid(int size)
        {
            //Populate game board with gamecell objects
            gameboard = new GameCell[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    gameboard[i, j] = new GameCell
                    {
                        Row = i,
                        Column = j
                    };
                }
            }
            activateCells();
            setNeighbors();
        }

        //Iterate through two-dimensional array and set roughly 15% of cells to LIVE
        private void activateCells()
        {
            Random rand = new Random();
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if (rand.Next(0, 100) <= 15)
                    {
                        gameboard[i, j].CellLive = true;
                    }
                }
            }
        }

        /* 
         *Iterate through two-dimensional array and if a LIVE cell is found, 
         *increment the value of surrounding NON-LIVE cells.
         *NON-LIVE cells contain value displaying how many LIVE cells are surrounding.
         *If cell is LIVE, count is set to 9.
        */
        private void setNeighbors()
        {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if (gameboard[i, j].CellLive == true)
                    {
                        gameboard[i, j].LiveNeighbors = 9;
                        for(int x = -1; x <= 1; x++)
                        {
                            for(int y = -1; y <= 1; y++)
                            {
                                if(i + x >= 0 && i + x < gameboard.GetLength(1) && j + y >= 0 && j + y < gameboard.GetLength(0))
                                {
                                    gameboard[i + x, j + y].LiveNeighbors++;
                                }
                            }
                        }                          
                    }
                }
            }
        }

        /*
         * Displays board in console. LIVE cells are marked with an asterisk.
         * NON-LIVE cells display integer value showing how many surrounding cells are LIVE.
        */
        public virtual void revealBoard()
        {
            //Display top border of table
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                Console.Write(" __ ");
            }
            Console.WriteLine("");

            //Display body of table
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if( j == gameboard.GetLength(1) - 1)
                    {
                        if (gameboard[i, j].CellLive == true)
                        {
                            Console.WriteLine("| * |");
                        }
                        else
                            Console.WriteLine("| " + gameboard[i, j].LiveNeighbors + " |");
                    }
                    else if (gameboard[i, j].CellLive == true)
                    {
                        Console.Write("| * ");
                    }
                    else
                        Console.Write("| " + gameboard[i, j].LiveNeighbors + " ");
                }
            }

            //Display bottom border of table
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                Console.Write(" __ ");
            }
            Console.WriteLine("");
        }
    }
}
