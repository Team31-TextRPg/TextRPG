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
        public List<Item> itemList;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 80, 100, 1500);
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
            new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true),
            new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true),
            new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true)
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
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine();

            int input = cu.GetInput(1, 3);
            switch (input)
            {
                case 1:
                    StatusScreen();
                    break;
                case 2:
                    BattleScreen();
                    break;
                case 3:
                    PotionScreen();
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
            for (int i = 0; i < battle.battleMonsters.Count; i++)
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
                if (battle.battleMonsters.Count > 0)
                {
                    for (int i = 0; i < battle.battleMonsters.Count; i++)
                    {
                        battle.PlayerAttack(input);
                    }
                }
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
            int input = cu.GetBattleOverInput(0, 0);

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

        public void PotionScreen()
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            int input = cu.GetInput(0, 1);
            switch(input)
            {
                case 0:
                    MainScreen();
                    break;
                case 1:
                    UsePotion(itemList[0]);
                    break;
            }
        }

       

        public void PlayerInventoryScreen()
        {
            if (itemList.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }

            Console.WriteLine("[나의 인벤토리]");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            // 인벤토리에 있는 아이템 출력
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {itemList[i].ItemDisplay()}");
            }
        }


        

        // 아이템 사용 매서드 
        public int UsePotion(Item item)
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.Write($"포션을 사용하면 체력을 30 회복 할 수 있습니다. ");
            Console.WriteLine();



            if (item.IsUsed && item.Type == Item.ItemType.HpPotion) // 선택된 item.IsUsed가 True 이고 아이템의 타입이 HpPotion 이면
            {
                player.health = Math.Min(player.health + item.Value, player.maxHealth);
                // *****플레이어의 HP가 상승 , 회복효과 설명, 아이템이 삭제
                if (player.health < player.maxHealth)
                {
                    UseHealthPotion(itemList[0]);
                    itemList.Remove(item); // 사용 후 아이템 삭제
                    Console.WriteLine($"(남은 포션 : {itemList.Count})");
                }
                else if(player.health == player.maxHealth)
                {
                    Console.WriteLine($"(남은 포션 : {itemList.Count})");
                }
            }
            else
            {
                Console.WriteLine($"{item.Name}은(는) 사용할 수 없습니다.");
            }
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            int input = cu.GetInput(0 , 1);
            switch (input)
            {
                case 0: 
                    PotionScreen();
                    break;
                case 1:
                    UsePotion(item);
                    break;
            }
            return player.health;

        }

        public void UseHealthPotion(Item Value)
        {
            player.health = Math.Min(player.health + 30, player.maxHealth);
        }

    }
}