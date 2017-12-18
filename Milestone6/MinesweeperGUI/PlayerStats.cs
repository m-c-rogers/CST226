using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Created by Michael Rogers - December 2017 - CST 227 taught by Professor Lori Collier

namespace MinesweeperGUI
{
    class PlayerStats : IComparable<PlayerStats>
    {
        private String playerName;
        private String levelPlayed;
        private int time;


        //Constructors
        public PlayerStats()
        {

        }

        public PlayerStats(string name, string difficulty, int time)
        {
            this.PlayerName = name;
            this.LevelPlayed = difficulty;
            this.Time = time;
        }

        //Accessors
        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }
        public string LevelPlayed
        {
            get => levelPlayed;
            set => levelPlayed = value;
        }
        public int Time
        {
            get => time;
            set => time = value;
        }

        //Interface implementation
        public int CompareTo(PlayerStats other)
        {
            if (this.Time < other.Time)
            {
                return 1;
            }
            else if (this.Time > other.Time)
            {
                return -1;
            }
            else
                return 0;
        }
    }
}
