using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface ISkill
    {
        public string name { get; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }
        public void SkillInfo();
        public void Use(Player player, Battle battle, int input);
    }
    public class AlphaStrike : ISkill
    {
        public string name { get; set; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }
        public AlphaStrike()
        {
            name = "알파 스트라이크";
            mpValue = 10;
            damageValue = 2;
            description = $"공격력 * {damageValue} 로 하나의 적을 공격합니다.";
            isRandom = false;
        }

        public void SkillInfo()
        {
            Console.WriteLine($"{name} - MP {mpValue}");
            Console.WriteLine(description);
        }

        public void Use(Player player, Battle battle, int input)
        {
            player.mp -= mpValue;

            int skillDamage = (int)Math.Ceiling(player.attack * 2.0f);

            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.WriteLine($"{player.character}의 공격! - 알파 스트라이크!!");
            Console.WriteLine();
            Console.Write($"{player.character} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");
            Console.WriteLine();
            Console.WriteLine($"LV.{battle.battleMonsters[input-1].Level} {battle.battleMonsters[input - 1].Name}");
            Console.Write($"HP {battle.battleMonsters[input - 1].Hp} -> ");

            battle.battleMonsters[input - 1].Hp -= skillDamage;

            if (battle.battleMonsters[input - 1].Hp <= 0)
            {
                battle.battleMonsters[input - 1].IsDead = true;
            }

            if (battle.battleMonsters[input - 1].IsDead)
            {
                ConsoleUtility.ColorWriteLine("Dead", ConsoleColor.DarkGray);
            }
            else
            {
                ConsoleUtility.ColorWriteLine($"{battle.battleMonsters[input - 1].Hp} ", ConsoleColor.DarkRed);
            }

            int deadCount = 0;

            for (int j = 0; j < battle.battleMonsters.Count; j++)
            {
                if (battle.battleMonsters[j].IsDead == true)
                {
                    deadCount++;
                }
            }

            if (deadCount == battle.battleMonsters.Count)
            {
                battle.isClear = 1;
            }
        }
    }

    public class DoubleStrike : ISkill
    {
        public string name { get; set; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }

        public DoubleStrike()
        {
            name = "더블 스트라이크";
            mpValue = 15;
            damageValue = 1.5f;
            description = $"공격력 * {damageValue} 로 2명의 적을 랜덤으로 공격합니다.";
            isRandom = true;
        }

        public void SkillInfo()
        {
            Console.WriteLine($"{name} - MP {mpValue}");
            Console.WriteLine(description);
        }

        public void Use(Player player, Battle battle, int input)
        {
            player.mp -= mpValue;

            Random rand = new Random();

            int randomIndex = 0;

            for (int i = 0; i < 2; i++)
            {
                do
                {
                    randomIndex = rand.Next(0, battle.battleMonsters.Count);
                }
                while (battle.battleMonsters[randomIndex].IsDead == true);

                int skillDamage = (int)Math.Ceiling(player.attack * 1.5f);

                Console.Clear();
                ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
                Console.WriteLine();
                Console.WriteLine($"{player.character}의 공격! - 더블 스트라이크!!");
                Console.WriteLine();
                Console.Write($"{player.character} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");
                Console.WriteLine();
                Console.WriteLine($"LV.{battle.battleMonsters[randomIndex].Level} {battle.battleMonsters[randomIndex].Name}");
                Console.Write($"HP {battle.battleMonsters[randomIndex].Hp} -> ");

                battle.battleMonsters[randomIndex].Hp -= skillDamage;

                if (battle.battleMonsters[randomIndex].Hp <= 0)
                {
                    battle.battleMonsters[randomIndex].IsDead = true;
                }

                if (battle.battleMonsters[randomIndex].IsDead)
                {
                    ConsoleUtility.ColorWriteLine("Dead", ConsoleColor.DarkGray);
                }
                else
                {
                    ConsoleUtility.ColorWriteLine($"{battle.battleMonsters[randomIndex].Hp} ", ConsoleColor.DarkRed);
                }

                int deadCount = 0;

                for (int j = 0; j < battle.battleMonsters.Count; j++)
                {
                    if (battle.battleMonsters[j].IsDead == true)
                    {
                        deadCount++;
                    }
                }

                Thread.Sleep(2000);

                if (deadCount == battle.battleMonsters.Count)
                {
                    battle.isClear = 1;
                    break;
                }
            }
        }
    }
}
