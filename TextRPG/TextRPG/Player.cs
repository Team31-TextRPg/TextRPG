﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TextRPG
{
    public class Player
    {
        public string character { get; private set; }
        public string jobClass { get; private set; }
        public int level { get; private set; }
        public int attack { get; private set; }
        public int defense { get; private set; }
        public int health { get; set; }
        public int maxHealth { get; private set; }
        public int gold { get; private set; }

        public Player(string name, string job, int level, int attackPower, int defensePower, int health, int maxhealth, int gold) // 플레이어 생성자
        {
            character = name;
            jobClass = job;
            this.level = level;
            attack = attackPower;
            defense = defensePower;
            this.health = health;
            maxHealth = maxhealth;
            this.gold = gold;
        }
        public void ShowStatus()
        {
            ConsoleUtility.Loading();
            Console.Clear();
            ConsoleUtility.ColorWriteLine("상태 보기", ConsoleColor.Cyan);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.Write("Lv. ");
            ConsoleUtility.ColorWriteLine($"{level.ToString("00")}", ConsoleColor.Red);
            Console.WriteLine($"{character} ( {jobClass} )");
            //Console.WriteLine($"공격력 : {attack}");
            Console.Write("공격력 : ");
            ConsoleUtility.ColorWriteLine($"{attack}", ConsoleColor.Red);
            //Console.WriteLine($"방어력 : {defense}");
            Console.Write("방어력 : ");
            ConsoleUtility.ColorWriteLine($"{defense}", ConsoleColor.Red);
            //Console.WriteLine($"체 력 : {health} / {maxHealth}");
            Console.Write("체 력 : ");
            ConsoleUtility.ColorWrite($"{health}", ConsoleColor.Red);
            Console.Write(" / ");
            ConsoleUtility.ColorWriteLine($"{maxHealth}", ConsoleColor.Red);
            //Console.WriteLine($"Gold : {gold} G");
            Console.Write("Gold : ");
            ConsoleUtility.ColorWrite($"{gold} " , ConsoleColor.Red);
            Console.WriteLine("G");
            Console.WriteLine("1");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

        }

    }
}
