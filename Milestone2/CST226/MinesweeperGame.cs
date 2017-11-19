using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST226
{
    class MinesweeperGame : Grid, IPlayable
    {
        private int userRow;
        private int userCol;

        public int UserCol
        {
            get => userCol;
            set => userCol = value;
        }
        public int UserRow
        {
            get => userRow;
            set => userRow = value;
        }

        public MinesweeperGame(int size) : base(size)
        {
        }

        //Initializes a Minesweeper game.
        public void playGame()
        {
            revealBoard();

            int winningGuesses = calculateWinningGuesses();
            int userGuesses = 0;
 
            bool continueGame = true;

            while (continueGame)
            {
                Console.WriteLine(" ");

                //Get user input for row and column. Validate both inputs are integers.
                Console.Write("Enter a row number >= 0 and <= " + (gameboard.GetLength(0) - 1) + ": ");
                var rowGuess = Console.ReadLine();
                if (int.TryParse(rowGuess, out userRow))
                {

                }
                else
                {
                    Console.WriteLine($"{rowGuess} is not a valid integer.");
                    continue;
                }
                

                Console.Write("Enter a column number >= 0 and <= " + (gameboard.GetLength(1) - 1) + ": ");
                var colGuess = Console.ReadLine();
                if (int.TryParse(colGuess, out userCol))
                {

                }
                else
                {
                    Console.WriteLine($"{colGuess} is not a valid integer.");
                    continue;
                }

                //Validate user input is within the range of board length.
                if (UserRow < 0 || UserRow > (gameboard.GetLength(0) - 1) || UserCol < 0 || UserCol > (gameboard.GetLength(1) - 1))
                {
                    Console.WriteLine("Invalid numbers entered. Please try again.");
                }
                else
                {
                    Console.Clear();
                    if (gameboard[UserRow, UserCol].CellVisited == false)
                    {
                        if (gameboard[UserRow, UserCol].CellLive == false)
                        {
                            /*
                             * If cell has not been visited and is not live, set cell to visited and display new board.
                             *Increment userGuesses by one. If userGuesses matches winningGuesses, end game and display message.
                             */
                            gameboard[UserRow, UserCol].CellVisited = true;
                            revealBoard();
                            userGuesses++;
                            if (userGuesses == winningGuesses)
                            {
                                continueGame = false;
                                Console.WriteLine("You have won!");
                            }
                        }
                        else
                        {
                            //If cell is live, end game and reveal entire board to user.
                            base.revealBoard();
                            Console.WriteLine("You hit a mine. Game over.");
                            continueGame = false;
                        }
                    }
                    else
                    {
                        //If cell has already been visited, inform user and have them pick another set of numbers.
                        Console.WriteLine("You have already picked this cell.");
                        Console.WriteLine(" ");
                        revealBoard();
                    }
                }
            }
        }

        public override void revealBoard()
        {
            /*Override revealBoard() method from Grid class
             * Display body of table.
             * Cells marked with a ? have not been visited.
             * Cells marked with a ~ have been visited but have no live neighbors.
             * Cells that have been visited and have live neighbors will display an integer value displaying # of live neighbors.
             */
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if (j == gameboard.GetLength(1) - 1)
                    {
                        if (gameboard[i, j].CellVisited == false)
                        {
                            Console.WriteLine("| ? |");
                        }
                        else if (gameboard[i, j].CellVisited == true)
                            if (gameboard[i, j].LiveNeighbors > 0)
                            {
                                Console.WriteLine("| " + gameboard[i, j].LiveNeighbors + " |");
                            }
                            else
                                Console.WriteLine("| ~ |");
                    }
                    else if (gameboard[i, j].CellVisited == false)
                    {
                        Console.Write("| ? ");
                    }
                    else
                        if (gameboard[i, j].LiveNeighbors > 0)
                        {
                            Console.Write("| " + gameboard[i, j].LiveNeighbors + " ");
                        }
                        else
                            Console.Write("| ~ ");
                }
            }
        }

        //Calculates the total number of guesses needed to complete a game.
        private int calculateWinningGuesses()
        {
            int guesses = 0;
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    if (gameboard[i, j].CellVisited == false && gameboard[i, j].CellLive == false)
                    {
                        guesses++;
                    }
                }
            }
            return guesses;
        }
    }
}
