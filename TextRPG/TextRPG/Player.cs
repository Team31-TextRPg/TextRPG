using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TextRPG
{
    public class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CurrentHealth { get; set; }
        public int Health {get; set; }
        public int Gold { get; set; } 

        public Player(string name, string job, int level, int atk, int def, int currenthealth, int health, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            CurrentHealth = currenthealth;
            Health = health;
            Gold = gold;
        }



    }
}
