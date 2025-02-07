using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class Battle
    {
        Player player;
        List<Monster> monsters;
        List<Monster> battleMonsters;

        //  Battle 클래스 생성자
        public Battle(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;
            battleMonsters = new List<Monster>();
        }

        //  전투 시작
        public void StartBattle()
        {
            //  랜덤 한 몬스터 1~3개 생성
            Random rand = new Random();

            List<Monster> randomMonsters = new List<Monster>();

            randomMonsters = monsters.OrderBy(x => rand.Next()).ToList();

            int monsterCount = rand.Next(0, 3);

            for (int i = 0; i < monsterCount; i++)
            {
                battleMonsters.Add(randomMonsters[i]);
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
        }

        //  적 몬스터의 턴, 적 몬스터의 공격력을 활용한 로직에 따라 플레이어의 hp 감소
        public void EnemyPhase()
        {
            for(int i = 0; i < battleMonsters.Count; i++)
            {
                if (battleMonsters[i].IsDead == false)
                {
                    Random rand = new Random();

                    float min = -1 * battleMonsters[i].Atk / 10.0f;
                    float max = battleMonsters[i].Atk / 10.0f;

                    int randomAttack = (int)Math.Ceiling(rand.NextDouble() * (max - min) + min);

                    player.health -= battleMonsters[i].Atk + randomAttack;
                }
            }
        }
    }
}
