using system;
using static Team31TextRPG.GameManager;
namespace Team31TextRPG;

// GameManager에서 Monster에 해당하는 파트만 만듬
class GameManager
{
    List<monster> monsterList;
    List<monster> battleStart = new List<monster>();

    //생성자, new GameManager() 호출시 실행
    public GameManager()
    {
        //Item(string name, ItemType type, int value, string descrip, int cost)
        monsterList = new List<monster>
        {
            new Monster(2, "미니언", 15, 5)
            new Monster(3, "공허충", 10, 9)
            new Monster(5, "대포미니언", 25, 8)

        };
    }

    public class Monster
    {
        //프로퍼티는 {get; set;}은 읽기+쓰기, {get;}은 읽기
        public int Level { get; }
        public string Name { get; }
        public int Hp { get; set; }
        public int MaxHp { get; }
        public int Atk { get; }
        public bool IsDead { get; set; }

        public Monster(int level, string name, int maxHp, int atk)
        {
            Level = level;
            Name = name;
            Hp = maxHp;
            MaxHp = maxHp;
            Atk = atk;

        }

        public string MonsterDisplay()
        {
            string str = IsEquip ? "[E]" : "";
            str += $"{Level} | {Name} | {GetTypeString()} | {Descrip} | {DeadString()}";
            return str;
        }

        public string DeadString()
        {
            string str = IsDead ? "Dead" : $"{Hp}";
            return str;
        }

    }
}

// 화면구성 - 시작, 상태, 전투 시작, 공격, Enemy Phase, 전투 결과
// 플레이어 - 레벨, 이름, 직업, 공격력, 장비공격력, 방어력, 장비방어력, 체력, 골드
// 몬스터 - 레벨, 이름, 체력(0이면 dead), 공격력
// 전투를 시작하면 <1~4마리>의 몬스터가 랜덤하게 등장
// 표시되는 순서는 랜덤
// <몬스터는 3종류> (중복해서 나타날 수 있음)





