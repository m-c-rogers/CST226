using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Created by Michael Rogers - December 2017 - CST 227 taught by Professor Lori Collier

namespace MinesweeperGUI
{
    class MinesweeperGame
    {
        //New array of buttons
        clickableButton[,] buttons;

        //High score collection
        List<PlayerStats> PlayerStats = new List<PlayerStats>();
        
        //Timer
        Timer gameTimer = new Timer();
        private int time = 0;
        Label displayTime = new Label();

        //For player stats
        private string playerName;
        private string level;

        //Define location document to store high scores
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public MinesweeperGame(GameBoard board, int x, int y, string playerInitials, string difficulty)
        {
            buttons = new clickableButton[x, y];
            playerName = playerInitials;
            level = difficulty;

            //Populate form with buttons.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    buttons[i, j] = new clickableButton(21 * i, 21 * j);
                    buttons[i, j].MouseDown += new MouseEventHandler(buttonClick);
                    buttons[i, j].Tag = i + ", " + j;
                    board.Controls.Add(buttons[i, j]);
                }
            }
            activateCells(buttons);
            setNeighbors(buttons);
            //Set timer interval, add Tick event, start timer
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(gameTimer_Tick);
            gameTimer.Start();

            //Change where Timer label displays on form based on size of grid.
            if(x == 10)
            {
                displayTime.Location = new Point(13, 218);
            }
            else if (x == 15)
            {
                displayTime.Location = new Point(13, 323);
            }
            else if (x == 20)
            {
                displayTime.Location = new Point(13, 428);
            }
            displayTime.AutoSize = true;
            displayTime.Text = "Time Elapsed: ";
            board.Controls.Add(displayTime);
        }

        //Set roughly 15% of cells to LIVE (bombs).
        private void activateCells(clickableButton[,] buttons)
        {
            Random rand = new Random();
            for(int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if(rand.Next(0,100) <= 15)
                    {
                        buttons[i, j].CellLive = true;
                    }
                }
            }
        }

        //Update each button with an integer to represent the number of LIVE neighbors the button has.
        private void setNeighbors(clickableButton[,] buttons)
        {
            for(int i = 0; i < buttons.GetLength(0); i++ )
            {
                for(int j = 0; j < buttons.GetLength(1); j++)
                {
                    if(buttons[i,j].CellLive == true)
                    {
                        buttons[i, j].LiveNeighbors = 9;
                        for(int x = -1; x <=1; x++)
                        {
                            for(int y = -1; y <= 1; y++)
                            {
                                if (i + x >= 0 && i + x < buttons.GetLength(1) && j + y >= 0 && j + y < buttons.GetLength(0))
                                {
                                    buttons[i + x, j + y].LiveNeighbors++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void buttonClick(Object sender, MouseEventArgs mea)
        {
            var button = sender as clickableButton;
            //Determine if mouse click is right button, if button has not been visited, set flag.
            if (mea.Button == MouseButtons.Right)
            {
                if (button.CellVisited == false)
                {
                    button.Image = Properties.Resources.flagged;
                }
            }
            else
            {
                //Parse index of button for purpose of passing it into revealCellBlocks()
                string[] btnIndex = button.Tag.ToString().Split(',');
                int row = Int32.Parse(btnIndex[0]);
                int col = Int32.Parse(btnIndex[1]);
                //If bomb is hit, game is over. Timer stops and message is displayed.
                if (button.CellLive == true)
                {
                    gameLost();
                    gameTimer.Stop();
                    MessageBox.Show("Game Over! Elapsed Time: " + time + " seconds.");
                }
                //If button has live neighbors, display integer value. Check if game has been won.
                else if (button.CellLive == false && button.LiveNeighbors > 0)
                {
                    button.Image = null;
                    button.CellVisited = true;
                    liveNeighbors(button);
                    //If game is won, replace bomb cells with flag images, stop timer, display message and disable all buttons.
                    if (gameWon())
                    {
                        replaceBombs();
                        gameTimer.Stop();
                        //Write player stats to file
                        using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\playerstats.txt", true))
                        {

                            outputFile.WriteLine(playerName + "," + level + "," + time);
                        }
                        readFile(PlayerStats);
                        //Sort PlayerStats by time and reverse the list
                        PlayerStats.Sort();
                        PlayerStats.Reverse();
                        //LINQ statement to query collection based on level being played. Takes first 5 entries.
                        var highScoreList = (from player in PlayerStats
                                             where player.LevelPlayed == level
                                             select player).Take(5);
                        //Display new form and populate the 5 entries
                        HighScore highscore = new HighScore();
                        highscore.dataGridView1.DataSource = highScoreList.ToList();
                        MessageBox.Show("You have won! Elapsed Time: " + time + " seconds.");
                        highscore.Show();
                        disableButtons();
                    }
                }
                else
                {
                    //Recursive method to reveal any adjacent NO NEIGHBOR cells
                    revealCellBlocks(row, col);
                    //If game is won, replace bomb cells with flag images, stop timer, display message and disable all buttons.
                    if (gameWon())
                    {
                        replaceBombs();
                        gameTimer.Stop();
                        //Write player stats to file
                        using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\playerstats.txt", true))
                        {

                            outputFile.WriteLine(playerName + "," + level + "," + time);
                        }
                        readFile(PlayerStats);
                        //Sort PlayerStats by time and reverse the list
                        PlayerStats.Sort();
                        PlayerStats.Reverse();
                        //LINQ statement to query collection based on level being played. Takes first 5 entries.
                        var highScoreList = (from player in PlayerStats
                                             where player.LevelPlayed == level
                                             select player).Take(5);
                        //Display new form and populate the 5 entries
                        HighScore highscore = new HighScore();
                        highscore.dataGridView1.DataSource = highScoreList.ToList();
                        MessageBox.Show("You have won! Elapsed Time: " + time + " seconds.");
                        highscore.Show();
                        disableButtons();
                    }
                }
            }

        }

        //Recursive method to reveal any adjacent NO NEIGHBOR cells
        public void revealCellBlocks(int row, int col)
        {
            if (buttons[row, col].CellVisited == false)
            {
                if (buttons[row, col].CellLive == false)
                {
                    if (buttons[row, col].LiveNeighbors == 0)
                    {
                        changeButton(buttons[row, col]);
                        if (row > 0) //Cell to left
                            revealCellBlocks(row - 1, col);
                        if (row < buttons.GetLength(0) - 1) //Cell to right
                            revealCellBlocks(row + 1, col);
                        if (col > 0) //Cell above
                            revealCellBlocks(row, col - 1);
                        if (col < buttons.GetLength(1) - 1) //Cell below
                            revealCellBlocks(row, col + 1);
                        if (row > 0 && col > 0) //Cell above and left
                            revealCellBlocks(row - 1, col - 1);
                        if (row < buttons.GetLength(0) - 1 && col > 0) //Cell below and left
                            revealCellBlocks(row + 1, col - 1);
                        if (row > 0 && col < buttons.GetLength(1) - 1) //Cell above and right
                            revealCellBlocks(row - 1, col + 1);
                        if (row < buttons.GetLength(0) - 1 && col < buttons.GetLength(1) - 1) //Cell below and right
                            revealCellBlocks(row + 1, col + 1);
                    }
                }
            }
        }

        //Runs if a bomb has been hit. Displays all cells and sets all LIVE cells to display bomb image.
        public void gameLost()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].CellLive == true)
                    {
                        buttons[i, j].Image = Properties.Resources.bomb;
                        buttons[i, j].CellLive = false;
                    }
                    else if (buttons[i, j].CellLive == false && buttons[i, j].CellVisited == false)
                    {
                        buttons[i, j].Image = null;
                        changeButton(buttons[i, j]);
                        liveNeighbors(buttons[i, j]);
                    }
                }
            }
        }

        //Checks if a game has been won.
        public bool gameWon()
        {
            int size = buttons.GetLength(0) * buttons.GetLength(1);
            int liveCells = 0;
            int guessesToWin;
            int visibleCells = 0;

            //Count number of live cells on board
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].CellLive == true)
                    {
                        liveCells++;
                    }
                }
            }

            guessesToWin = size - liveCells;

            //Calculate number of visible cells
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].CellVisited == true)
                    {
                        visibleCells++;
                    }
                }
            }

            //If guessesToWin equals visibleCells, game has been won.
            if (guessesToWin == visibleCells)
            {
                return true;
            }
            else
                return false;
        }

        //Replaces all LIVE cells with flag images.
        public void replaceBombs()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].CellLive == true)
                    {
                        buttons[i, j].Image = Properties.Resources.flagged;
                    }

                }
            }
        }

        //Method to change the text of a button to match # of LIVE neighbors.
        public void liveNeighbors(clickableButton button)
        {
            switch (button.LiveNeighbors)
            {
                case 0: changeButton(button);
                    break;
                case 1:
                    button.Text = "1"; button.ForeColor = Color.Blue;
                    break;
                case 2:
                    button.Text = "2"; button.ForeColor = Color.Green;
                    break;
                case 3:
                    button.Text = "3"; button.ForeColor = Color.Red;
                    break;
                case 4:
                    button.Text = "4"; button.ForeColor = Color.DarkBlue;
                    break;
                case 5:
                    button.Text = "5"; button.ForeColor = Color.DarkGreen;
                    break;
                case 6:
                    button.Text = "6"; button.ForeColor = Color.DarkRed;
                    break;
                case 7:
                    button.Text = "7"; button.ForeColor = Color.Yellow;
                    break;
                case 8:
                    button.Text = "8"; button.ForeColor = Color.Orange;
                    break;
            }
        }

        //Method to change appearance of a button.
        public void changeButton(clickableButton button)
        {
            button.CellVisited = true;
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.LightGray;
        }

        //Disables buttons from being clicked.
        public void disableButtons()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j].Enabled = false;
                }
            }
        
        }

        //Tick event for timer.
        private void gameTimer_Tick(object board, EventArgs e)
        {
            time++;
            displayTime.Text = "Time Elapsed: " + time.ToString() + " seconds.";
        }

        //Reads and uses data from external file to populate List<PlayerStats> with new objects
        public static void readFile(List<PlayerStats> playerStats)
        {
            string line;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = Path.Combine(path, "playerstats.txt");
            StreamReader sr = new StreamReader(filename);
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                playerStats.Add(new PlayerStats(words[0], words[1], Convert.ToInt32(words[2])));
            }
        }
        
    }
}
