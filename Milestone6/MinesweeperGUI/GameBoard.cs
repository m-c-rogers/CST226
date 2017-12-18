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
        MinesweeperGame board;

        public GameBoard()
        {
            InitializeComponent();
        }

        public GameBoard(int x, string playerInitials, string difficulty)
        {
            InitializeComponent();
            this.FormClosing += GameBoard_FormClosing;
            //Generate board size based on size passed in from StartScreen.
            if (x == 10)
            {
                this.Width = 225;
                this.Height = 278;
                board = new MinesweeperGame(this, x, x, playerInitials, difficulty);
            }
            else if (x == 15)
            {
                this.Width = 330;
                this.Height = 383;
                board = new MinesweeperGame(this, x, x, playerInitials, difficulty);
            }
            else if ( x == 20)
            {
                this.Width = 435;
                this.Height = 488;
                board = new MinesweeperGame(this, x, x, playerInitials, difficulty);
            }
            //Prevent resizing of form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {

        }
    }
}

