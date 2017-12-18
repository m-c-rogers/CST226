using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Created by Michael Rogers - December 2017 - CST 227 taught by Professor Lori Collier

namespace MinesweeperGUI
{
    class clickableButton : Button
    {
        const int height = 20;
        const int width = 20;
        private int row;
        private int column;
        private bool cellVisited;
        private bool cellLive;
        private int liveNeighbors;

        //Constructors
        public clickableButton()
        {

        }

        public clickableButton(int xCoord, int yCoord)
        {
            this.Location = new Point(xCoord, yCoord);
            this.Height = height;
            this.Width = width;
            this.FlatStyle = FlatStyle.Popup;
            this.row = -1;
            this.column = -1;
            this.cellVisited = false;
            this.cellLive = false;
            this.liveNeighbors = 0;
            this.BackColor = Color.DarkGray;
            this.Font = new Font(Button.DefaultFont.FontFamily, 6);
        }

        //Accessors
        public int Row
        {
            get => row;
            set => row = value;
        }
        public int Column
        {
            get => column;
            set => column = value;
        }
        public bool CellVisited
        {
            get => cellVisited;
            set => cellVisited = value;
        }
        public bool CellLive
        {
            get => cellLive;
            set => cellLive = value;
        }
        public int LiveNeighbors
        {
            get => liveNeighbors;
            set => liveNeighbors = value;
        }

        
        //Add a blue border and flatten button when clicked.
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.LightGray;
        }
        
    }
}
