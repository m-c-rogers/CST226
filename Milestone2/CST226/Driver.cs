using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Created by Michael Rogers - November 2017 - Class CST 227 taught by Lori Collier

namespace CST226
{
    class Driver
    {
        static void Main(string[] args)
        {
            //Initialize a 10x10 game board and start game.
            MinesweeperGame board = new MinesweeperGame(10);
            board.playGame();
        }
    }
}
