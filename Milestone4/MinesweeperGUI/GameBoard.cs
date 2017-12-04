using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Created by Michael Rogers - December 2017 - CST 227 taught by Professor Lori Collier

namespace MinesweeperGUI
{
    public partial class GameBoard : Form
    {

        public GameBoard()
        {
            InitializeComponent();
        }

        public GameBoard(int x, int y)
        {
            InitializeComponent();
            //Generate board size based on size passed in from StartScreen.
            if (x == 10)
            {
                this.Width = 225;
                this.Height = 248;
            }
            else if (x == 15)
            {
                this.Width = 330;
                this.Height = 353;
            }
            else if ( x == 20)
            {
                this.Width = 435;
                this.Height = 458;
            }
            //Prevent resizing of form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            //New array of buttons
            clickableButton[,] buttons = new clickableButton[x, y];

            //Populate form with buttons.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    buttons[i, j] = new clickableButton(21 * i, 21 * j);
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void GameBoard_Deactivate(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

