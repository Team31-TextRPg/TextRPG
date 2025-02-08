using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG

{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();

            gm.MainScreen();
        }
    }

    //  게임이 시작될 때 필요한 모든 것들을 생성하는 클래스
    public class GameManager
    {
        Battle battle;
        Player player;
        List<Monster> monsterList;
        
        ConsoleUtility cu;
        List<Item> itemList;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 100, 100, 1500);
            cu = new ConsoleUtility();
     
            monsterList = new List<Monster>
            {
                new Monster(2,"미니언",15, 10),
                new Monster(3,"공허충", 10, 15),
                new Monster(5,"대포미니언",25, 15)
            };
            battle = new Battle(player, monsterList);
            itemList = new List<Item>
        {
            new Item(name: "작은 붉은 포션", type: Item.ItemType.HpPotion, value: 10, description: "10 HP 회복됩니다.", isUsed: true),
            new Item(name: "큰 붉은 포션", type: Item.ItemType.HpPotion, value: 20, description: "20 HP 회복됩니다.", isUsed: false)
        };
        }

        public void MainScreen()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine();

            int input = cu.GetInput(1, 2);
            switch (input)
            {
                case 1: StatusScreen();
                    break;
                case 2: BattleScreen();
                    break;
            }
        }
        public void StatusScreen()
        {
            player.ShowStatus();
            int input = cu.GetInput(0, 0);
            MainScreen();
   
        }

        public void BattleScreen()
        {
           
            Console.Clear();
            battle.StartBattle();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for(int i = 0; i < battle.battleMonsters.Count; i++)
            {
                Console.WriteLine(battle.battleMonsters[i].MonsterDisplay());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level}  {player.character} ({player.jobClass})");
            Console.WriteLine($"HP {player.health}/{player.maxHealth}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            int input = cu.GetInput(1, 1);
            AttackScreen(battle);

        }
        public void AttackScreen(Battle battle)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < battle.battleMonsters.Count; i++)
            {
                Console.WriteLine($"{i + 1} " + battle.battleMonsters[i].MonsterDisplay());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level}  {player.character} ({player.jobClass})");
            Console.WriteLine($"HP {player.health}/{player.maxHealth}");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            int input = cu.GetBattleInput(0, battle.battleMonsters.Count);
             if (input == 0)
             {
                 BattleScreen();
             }
             else if (input >= 1 && input <= battle.battleMonsters.Count)
             {
                 battle.PlayerAttack(input);
             }
            

        }
        public void BattleResultWin()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {battle.battleMonsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.level} {player.jobClass}");
            Console.WriteLine($"HP {player.maxHealth} -> {player.health}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
            int input = cu.GetBattleOverInput(0 , 0);

        }

        public void BattleResultLose()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.level} {player.jobClass}");
            Console.WriteLine($"HP {player.maxHealth} -> {player.health}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
            int input = cu.GetBattleOverInput(0, 0);

        }
        public void PlayerInventoryScreen()
        {
            if (itemList.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            Console.WriteLine("[나의 인벤토리]");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // 인벤토리에 있는 아이템 출력
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {itemList[i].ItemDisplay()}");
            }

            Console.WriteLine("\n사용할 아이템 번호를 입력하세요 (0을 입력하면 취소)");
            int choice;

            // *****ConsoleUtility 으로 넣어야 함.
            // 입력 오류 방지
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > itemList.Count)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            }

            if (choice == 0)
            {
                Console.WriteLine("아이템 사용이 취소되었습니다.");
                return;
            }

            UseItem(itemList[choice - 1]);

        }

        // 아이템 사용 매서드 
        public void UseItem(Item item)
        {
            if (item.IsUsed && item.Type == Item.ItemType.HpPotion) // 선택된 item.IsUsed가 True 이고 아이템의 타입이 HpPotion 이면
            {
                // *****플레이어의 HP가 상승 , 회복효과 설명, 아이템이 삭제
                Console.WriteLine($"{item.Name} 사용! HP +{item.Description}"); // 회복 효과 설명
                itemList.Remove(item); // 사용 후 아이템 삭제

            }
            else
            {
                Console.WriteLine($"{item.Name}은(는) 사용할 수 없습니다.");
            }
        }

    }