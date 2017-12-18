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
            //Verify initials have been entered.
            if (String.IsNullOrEmpty(playerInitials.Text))
            {
                MessageBox.Show("Initials are required.");
            }
            //Verify that a difficulty is selected.
            else if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                MessageBox.Show("Please select a difficulty");
            }
            else
            {
                //Start new Minesweeper game. Board size, player's initials, and difficulty level passed to new form.

                if (radioButton1.Checked)
                {
                    GameBoard newGame = new GameBoard(10, playerInitials.Text, "easy");
                    newGame.Show();
                }
                else if (radioButton2.Checked)
                {
                    GameBoard newGame = new GameBoard(15, playerInitials.Text, "medium");
                    newGame.Show();
                }
                else if (radioButton3.Checked)
                {
                    GameBoard newGame = new GameBoard(20, playerInitials.Text, "hard");
                    newGame.Show();
                }
                this.Hide();
            }
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
