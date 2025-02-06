using System.Numerics;

namespace Monster
{
    class Program
    {
        // 화면구성 - 시작, 상태, 전투 시작, 공격, Enemy Phase, 전투 결과
        // 플레이어 - 레벨, 이름, 직업, 공격력, 장비공격력, 방어력, 장비방어력, 체력, 골드
        // 몬스터 - 레벨, 이름, 체력(0이면 dead), 공격력 

        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            gm.BattleScreen();
            Console.ReadKey();
            
        }
    }

    // 실행되는지 테스트해보려고, GameManager에서 Monster에 해당하는 파트만 만들었습니다.
    class GameManager
    {
        public List<Monster> monsterList;
        public List<Monster> battleStart = new List<Monster>();

        //생성자, new GameManager() 호출시 실행
        public GameManager()
        {
            monsterList = new List<Monster>
        {
            new Monster(2, "미니언", 15, 5),
            new Monster(3, "공허충", 10, 9),
            new Monster(5, "대포미니언", 25, 8)
        };
        }

        public void BattleScreen() // 2. 전투 시작
        {
            Console.WriteLine("Battle!");
            Console.WriteLine("");
            Console.WriteLine(monsterList[0].MonsterDisplay()); // 테스트용
            Console.WriteLine(monsterList[1].MonsterDisplay()); // 테스트용
            Console.WriteLine(monsterList[2].MonsterDisplay()); // 테스트용

        }

        public void MonsterDead(Monster monster) // Monster의 죽음 판별
        {
            int playerAtk = 10; // 테스트용 임시 변수입니다.

            // Hp <= 0 
            if (monster.Hp > 0)
            {
                monster.Hp -= playerAtk;
                BattleScreen();
            }
            else // Hp > 0
            {
                BattleScreen(); // 죽었다는 메세지 표기
            }
        }

    }
}
