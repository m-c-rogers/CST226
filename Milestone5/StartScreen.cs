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
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //Start new Minesweeper game. Size of board is passed to new form based on user's difficulty selection.
             
            if (radioButton1.Checked)
            {
                GameBoard newGame = new GameBoard(10);
                newGame.Show();             
            }
            else if (radioButton2.Checked)
            {
                GameBoard newGame = new GameBoard(15);
                newGame.Show();               
            }
            else if (radioButton3.Checked)
            {
                GameBoard newGame = new GameBoard(20);
                newGame.Show();
            }
            this.Hide();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
