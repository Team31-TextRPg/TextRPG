using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
<<<<<<< Updated upstream
    internal class Player
    {
        public int level;
        public string character;
        public float attack;
        public float defense;
        public float health; //현재 체력
        public float maxHealth; //전체 체력
        public int gold;

        //  Player 클래스 생성자
        public Player(String name)
        {
            level = 1;
            character = name;
            attack = 10;
            defense = 5;
            health = 100;
            gold = 1500;
        }

            public void ShowStatus() //상태보기
        {
            Console.WriteLine($"[ {character} ]");
            Console.WriteLine($"레벨: {level}");
            Console.WriteLine($"체력: {health}/{maxHealth}");
            Console.WriteLine($"공격력: {attack}");
            Console.WriteLine($"방어력: {defense}");
        }
    }
    }
=======
    public string character { get; private set; }
    public string jobClass { get; private set; }
    public int level { get; private set; }
    public int attack { get; private set; }
    public int defense { get; private set; }
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int gold { get; private set; }

    public Player(string name, string job, int level, int attackPower, int defensePower, int health, int gold) // 플레이어 생성자
    {
        character = name;
        jobClass = job;
        level = level;
        attack = attackPower;
        defense = defensePower;
        maxHealth = health;
        gold = gold;
    }

    public void ShowStatus()
    {
        Console.WriteLine($"Lv. {level}");
        Console.WriteLine($"{character} ({jobClass})");
        Console.WriteLine($"공격력: {attack}");
        Console.WriteLine($"방어력: {defense}");
        Console.WriteLine($"체 력: {health} / {maxHealth}");
        Console.WriteLine($"Gold: {gold} G");
    }
     
>>>>>>> Stashed changes
}
