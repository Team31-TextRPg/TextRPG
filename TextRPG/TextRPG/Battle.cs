using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class Battle
    {
        public bool start { get; set; }
        public int isClear { get; set; }
        //  isClear == 0 이면 전투 진행중
        //             1 이면 성공
        //             2 면 실패
        public Player player { get; set; }
        public List<Monster> monsters { get; set; }
        public List<Monster> battleMonsters { get; set; }

        //  Battle 클래스 생성자
        public Battle(Player player, List<Monster> monsters)
        {
            start = false;
            isClear = 0;
            this.player = player;
            this.monsters = monsters;
            battleMonsters = new List<Monster>();
        }


        //  전투 시작
        public void StartBattle()
        {
            //  랜덤한 몬스터가 1~3개 생성되는 로직
            Random rand = new Random();

            List<Monster> randomMonsters = new List<Monster>();

            randomMonsters = monsters.OrderBy(x => rand.Next()).ToList();

            int monsterCount = rand.Next(1, 4);

            while (true)
            {
                if (battleMonsters.Count == monsterCount)
                {
                    break;
                }

                battleMonsters.Add(randomMonsters[rand.Next(0, randomMonsters.Count)]);
            }
        }

        //  플레이어의 공격, 공격할 몬스터의 번호를 입력받으면 로직에 따라 그 몬스터의 hp 감소
        public void PlayerAttack(int monsterNum)
        {
            Random rand = new Random();

            float min = -1 * player.attack / 10.0f;
            float max = player.attack / 10.0f;

            int randomAttack = (int)player.attack + (int)Math.Round(rand.NextDouble() * (max - min) + min);

            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.WriteLine($"{player.character} 의 공격!");
            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{battleMonsters[monsterNum - 1].Level} ", ConsoleColor.DarkRed);
            Console.Write($"{ battleMonsters[monsterNum - 1].Name} 을(를) 맞췄습니다. [데미지 : ");
            ConsoleUtility.ColorWrite( $"{randomAttack}", ConsoleColor.DarkRed);
            Console.WriteLine("]");
            Console.WriteLine();
            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{battleMonsters[monsterNum - 1].Level} ", ConsoleColor.DarkRed);
            Console.WriteLine($"{battleMonsters[monsterNum - 1].Name}");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{battleMonsters[monsterNum - 1].Hp} ", ConsoleColor.DarkRed);
            Console.Write("-> ");

            battleMonsters[monsterNum - 1].Hp -= randomAttack;

            if (battleMonsters[monsterNum - 1].Hp <= 0)
            {
                battleMonsters[monsterNum - 1].IsDead = true;
            }

            if (battleMonsters[monsterNum - 1].IsDead)
            {
                ConsoleUtility.ColorWriteLine("Dead" , ConsoleColor.DarkGray);
            }
            else
            {
                ConsoleUtility.ColorWriteLine($"{battleMonsters[monsterNum - 1].Hp} ", ConsoleColor.DarkRed);
            }

            //  현재 몇 마리의 몬스터가 죽었는지 계산
            int deadCount = 0;

            for (int i = 0; i < battleMonsters.Count; i++)
            {
                if (battleMonsters[i].IsDead == true)
                {
                    deadCount++;
                }
            }

            //  몬스터가 전부 죽었으면 던전 클리어
            if (deadCount == battleMonsters.Count)
            {
                isClear = 1;
            }
        }

        //  적 몬스터의 턴, 적 몬스터의 공격력을 활용한 로직에 따라 플레이어의 hp 감소
        public void EnemyPhase()
        {
            for (int i = 0; i < battleMonsters.Count; i++)
            {
                if (battleMonsters[i].IsDead == false)
                {
                    Random rand = new Random();

                    float min = -1 * battleMonsters[i].Atk / 10.0f;
                    float max = battleMonsters[i].Atk / 10.0f;

                    int randomAttack = battleMonsters[i].Atk + (int)Math.Round(rand.NextDouble() * (max - min) + min);

                    Console.Clear();
                    ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
                    Console.WriteLine();
                    Console.Write("Lv.");
                    ConsoleUtility.ColorWrite($"{battleMonsters[i].Level} ", ConsoleColor.DarkRed);
                    Console.WriteLine($"{ battleMonsters[i].Name} 의 공격!");
                    Console.Write($"{player.character} 을(를) 맞췄습니다. [데미지 : ");
                    ConsoleUtility.ColorWrite($"{randomAttack}", ConsoleColor.DarkRed);
                    Console.WriteLine("]");
                    Console.WriteLine();
                    Console.Write("Lv.");
                    ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
                    Console.WriteLine($"{player.character}");
                    Console.Write($"HP ");
                    ConsoleUtility.ColorWrite($"{player.health} ", ConsoleColor.DarkRed);
                    Console.Write("-> ");


                    player.health -= randomAttack;

                    if (player.health <= 0)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        ConsoleUtility.ColorWriteLine($"{player.health} ", ConsoleColor.DarkRed);
                    }

                    Thread.Sleep(2000);

                    if (player.health <= 0)
                    {
                        isClear = 2;
                    }
                }
            }
        }
    }
}
