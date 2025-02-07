using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
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
}
