using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class Battle
    {
        public bool isClear { get; set; }
        public Player player { get; set; }
        public List<Monster> monsters { get; set; }
        public List<Monster> battleMonsters { get; set; }

        //  Battle 클래스 생성자
        public Battle(Player player, List<Monster> monsters)
        {
            isClear = false;
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
                if(battleMonsters.Count == monsterCount)
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

            int randomAttack = (int)Math.Ceiling(rand.NextDouble() * (max - min) + min);

            battleMonsters[monsterNum - 1].Hp -= (int)player.attack + randomAttack;

            if (battleMonsters[monsterNum - 1].Hp <= 0)
            {
                battleMonsters[monsterNum - 1].IsDead = true;
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

                isClear = true;
                
                //  전투 결과 창으로 이동
                //  isClear가 true이기 때문에 Victory창으로 이동
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

                    int randomAttack = (int)Math.Ceiling(rand.NextDouble() * (max - min) + min);

                    player.health -= battleMonsters[i].Atk + randomAttack;

                    if(player.health <= 0)
                    {
                        isClear = false;
                        //  전투 결과 창으로 이동
                        //  isClear가 false이기 때문에 You Lose창으로 이동
                    }
                }
            }
        }
    }
}
