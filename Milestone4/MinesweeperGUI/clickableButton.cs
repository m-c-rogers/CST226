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
        }

        //Add a blue border and flatten button when clicked.
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 2;
            this.FlatAppearance.BorderColor = Color.Blue;
        }
    }
}
