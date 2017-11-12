using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Created by Michael Rogers - November 2017 - Class CST 227 taught by Lori Collier

namespace CST226
{
    class GameCell
    {

        private int row;
        private int column;
        private bool cellVisited;
        private bool cellLive;
        private int liveNeighbors;

        //Constructor
        public GameCell()
        {
            this.row = -1;
            this.column = -1;
            this.cellVisited = false;
            this.cellLive = false;
            this.liveNeighbors = 0;
        }

        //Accessors
        public int Row {
            get => row;
            set => row = value;
        }
        public int Column {
            get => column;
            set => column = value;
        }
        public bool CellVisited {
            get => cellVisited;
            set => cellVisited = value;
        }
        public bool CellLive {
            get => cellLive;
            set => cellLive = value;
        }
        public int LiveNeighbors {
            get => liveNeighbors;
            set => liveNeighbors = value;
        }
    }
}
