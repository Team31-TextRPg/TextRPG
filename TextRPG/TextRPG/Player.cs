﻿using Newtonsoft.Json;
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
        public int floor { get; set; }
        public string character { get; set; }
        public string jobClass { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int maxExp { get; set; }
        public float attack { get; set; }
        public int defense { get; set; }
        public int health { get; set; }
        public List<ISkill> skillList { get; set; }
        public int maxHealth { get; private set; } 
        public int mana { get; set; } 
        public int maxMana { get; private set; } 
        public int gold { get; set; }

        public Player(string name, string job, int level, int attackPower, int defensePower, int health, int maxhealth, int mana, int maxmana, int gold) // 플레이어 생성자 
        {
            floor = 1;
            character = name;
            jobClass = job;
            this.level = level;
            exp = 0;
            maxExp = 10;
            attack = attackPower;
            defense = defensePower;
            this.health = health;
            maxHealth = maxhealth;
            this.mana = mana; 
            maxMana = maxmana; 
            this.gold = gold;
            skillList = new List<ISkill>
            {
                new AlphaStrike(),
                new DoubleStrike()
            };
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
            Console.Write("Exp : ");
            ConsoleUtility.ColorWrite($"{exp}", ConsoleColor.Red);
            Console.Write(" / ");
            ConsoleUtility.ColorWriteLine($"{maxExp}", ConsoleColor.Red);
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
            Console.Write("마 력 : ");
            ConsoleUtility.ColorWrite($"{mana}", ConsoleColor.Red);
            Console.Write(" / ");
            ConsoleUtility.ColorWriteLine($"{maxMana}", ConsoleColor.Red);
            //Console.WriteLine($"Gold : {gold} G");
            Console.Write("Gold : ");
            ConsoleUtility.ColorWrite($"{gold} ", ConsoleColor.Red);
            Console.WriteLine("G");
            Console.WriteLine("1");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

        }

        public void Warrior()
        {
            jobClass = "전사";
            attack = 10;
            defense = 5;
            maxHealth = 130; // 직업 전사일 때, HP 올라감
            health = maxHealth;
            maxMana = 100; 
            mana = maxMana; 
        }

        public void Archer()
        {
            jobClass = "궁수";
            attack = 14; // 직업 궁수일 때, 공격력 올라감
            defense = 5;
            maxHealth = 100;
            health = maxHealth;
            maxMana = 100; 
            mana = maxMana; 
        }

        public void Thief()
        {
            jobClass = "도적";
            attack = 10;
            defense = 9; // 도적일 때, 방어력 올라감
            maxHealth = 100;
            health = maxHealth;
            maxMana = 100; 
            mana = maxMana; 
        }

        //  Player 클래스의 데이터를 저장하는 함수
        public void Save(string filePath)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SkillConverter());
            string json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(filePath, json);
        }

        //  Player 클래스의 데이터를 불러오는 함수
        public static Player Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SkillConverter());
            return JsonConvert.DeserializeObject<Player>(json, settings);
        }
    }
}
