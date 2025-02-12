using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class Battle
    {
        public int floor { get; set; }
        public bool start { get; set; }
        public int isClear { get; set; }
        //  isClear == 0 이면 전투 진행중
        //             1 이면 성공
        //             2 면 실패
        public Player player { get; set; }
        public List<Monster> monsters { get; set; }
        public List<Monster> battleMonsters { get; set; }


        //  Battle 클래스 생성자
        public Battle(Player player, List<Monster> monsters, int floor)
        {
            start = false;
            isClear = 0;
            this.player = player;
            this.monsters = monsters;
            this.floor = floor;
            battleMonsters = new List<Monster>();
        }

        //  전투 시 15% 확률로 크리티컬 데미지를 입힌다.
        public bool CriticalAttack()
        {
            Random rand = new Random();

            int cri = rand.Next(0, 100);

            if (cri < 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //  전투 시 10% 확률로 미스(회피) 판정
        public bool MissAttack()
        {
            Random rand = new Random();

            int cri = rand.Next(0, 100);

            if (cri < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //  전투 시작
        public void StartBattle()
        {
            //  랜덤한 몬스터가 1~3개 생성되는 로직
            Random rand = new Random();

            int monsterCount = rand.Next(floor, floor + 4);

            while (true)
            {
                if (battleMonsters.Count == monsterCount)
                {
                    break;
                }

                battleMonsters.Add((Monster)monsters[rand.Next(floor - 1, Math.Min(floor + 2, monsters.Count))].Clone());
            }
        }

        //  플레이어의 공격, 공격할 몬스터의 번호를 입력받으면 로직에 따라 그 몬스터의 hp 감소
        public void PlayerAttack(int monsterNum)
        {
            Random rand = new Random();

            int attackMin = (int)player.attack - (int)Math.Ceiling(player.attack / 10.0f);
            int attackMax = (int)player.attack + (int)Math.Ceiling(player.attack / 10.0f);

            int randomAttack = rand.Next(attackMin, attackMax + 1);

            bool isCritical = CriticalAttack();
            if (isCritical)
            {
                randomAttack *= 16;
                randomAttack /= 10;
            }

            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.WriteLine($"{player.character} 의 공격!");

            if (MissAttack())
            {
                Console.Write($"LV.{battleMonsters[monsterNum - 1].Level} {battleMonsters[monsterNum - 1].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Console.Write($"LV.{battleMonsters[monsterNum - 1].Level} {battleMonsters[monsterNum - 1].Name} 을(를) 맞췄습니다. [데미지 : {randomAttack}]");

                if (isCritical)
                {
                    Console.WriteLine(" - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine();
                }


                Console.WriteLine();
                Console.WriteLine($"LV.{battleMonsters[monsterNum - 1].Level} {battleMonsters[monsterNum - 1].Name}");
                Console.Write($"HP {battleMonsters[monsterNum - 1].Hp} -> ");

                battleMonsters[monsterNum - 1].Hp -= randomAttack;

                if (battleMonsters[monsterNum - 1].Hp <= 0)
                {
                    battleMonsters[monsterNum - 1].IsDead = true;
                }

                if (battleMonsters[monsterNum - 1].IsDead)
                {
                    ConsoleUtility.ColorWriteLine("Dead", ConsoleColor.DarkGray);
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
        }

        //  적 몬스터의 턴, 적 몬스터의 공격력을 활용한 로직에 따라 플레이어의 hp 감소
        public void EnemyPhase()
        {
            for (int i = 0; i < battleMonsters.Count; i++)
            {
                if (battleMonsters[i].IsDead == false)
                {
                    Random rand = new Random();

                    int attackMin = (int)battleMonsters[i].Atk - (int)Math.Ceiling(battleMonsters[i].Atk / 10.0f);
                    int attackMax = (int)battleMonsters[i].Atk + (int)Math.Ceiling(battleMonsters[i].Atk / 10.0f);

                    int randomAttack = rand.Next(attackMin, attackMax + 1);

                    bool isCritical = CriticalAttack();
                    if (isCritical)
                    {
                        randomAttack *= 16;
                        randomAttack /= 10;
                    }

                    Console.Clear();
                    ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
                    Console.WriteLine();
                    Console.WriteLine($"LV.{battleMonsters[i].Level} {battleMonsters[i].Name} 의 공격!");

                    if (MissAttack())
                    {
                        Console.Write($"{player.character} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                    }
                    else
                    {
                        Console.Write($"{player.character} 을(를) 맞췄습니다. [데미지 : {randomAttack}]");

                        if (isCritical)
                        {
                            Console.WriteLine(" - 치명타 공격!!");
                        }
                        else
                        {
                            Console.WriteLine();
                        }

                        Console.WriteLine();
                        Console.WriteLine($"LV.{player.level} {player.character}");
                        Console.Write($"HP {player.health} -> ");

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
}

