using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG

{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            gm.IntroScreen();

        }
    }

    //  게임이 시작될 때 필요한 모든 것들을 생성하는 클래스
    public class GameManager
    {

        ConsoleUtility cu;

        Battle battle;
        Player player;
        QuestInfo questInfo;

        List<Monster> monsterList;
        public List<Item> itemList;
        public List<QuestInfo> questList;    // 퀘스트 전체 정보

        ConsoleUtility cu;

        Inventory inventory;
        Item item;
        int floor;
        Quest quest;
        QuestInfo questinfo;

        List<Monster> monsterList;
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

            itemList = new List<Item>
            {
                new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true),
                new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true),
                new Item(name: "붉은 포션", type: Item.ItemType.HpPotion, value: 30, description: "30 HP 회복됩니다.", isUsed: true)
            };

            questList = new List<Quest>
            {
                new Quest("마을을 위협하는 미니언 처치", "미니언 5마리 처치 (0/5)", 5, "쓸만한 방패 x1", 5)
            };
        }

        public void IntroScreen()
        {
            ConsoleUtility.Loading();
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 입력해주세요:  ");
            string playername = Console.ReadLine();
            player.character = playername;
            Console.WriteLine();
            int playerJob = cu.JobInput(1, 3);
            switch (playerJob)
            {
                case 1:
                    player.Warrior();
                    break;
                case 2:
                    player.Archer();
                    break;
                case 3:
                    player.Theif();
                    break;
            }
            MainScreen();
        }

        public void MainScreen()
        {
            Console.Clear();
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 회복 아이템");
            Console.WriteLine($"2. 전투 시작 (현재 진행 : {floor}층)");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine();

            int input = cu.GetInput(1, 4);
            switch (input)
            {
                case 1:
                    StatusScreen();
                    break;
                case 2:
                    battle = new Battle(player, monsterList);
                    BattleScreen(battle);
                    break;
                case 3:
                    PotionScreen();
                    break;
                case 4:
                    QuestScreen();
                    break;
                case 4:
                    QuestScreen();
                    break;
            }
        }


        public void StatusScreen()
        {
            player.ShowStatus();
            int input = cu.GetInput(0, 0);
            MainScreen();

        }

        public void BattleScreen(Battle battle)
        {

            ConsoleUtility.Loading();

            Console.Clear();
            if (battle.start == false)
            {
                battle.StartBattle();
                battle.start = true;
            }


            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);

            Console.WriteLine();
            for (int i = 0; i < battle.battleMonsters.Count; i++)
            {
                if (battle.battleMonsters[i].IsDead == true)
                {
                    ConsoleUtility.ColorWriteLine(battle.battleMonsters[i].MonsterDisplay(), ConsoleColor.DarkGray);
                }
                else
                {
                    Console.WriteLine(battle.battleMonsters[i].MonsterDisplay());
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
            Console.WriteLine($" {player.character} ({player.jobClass})");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{player.health}", ConsoleColor.DarkRed);
            Console.Write("/");
            ConsoleUtility.ColorWriteLine($"{player.maxHealth} ", ConsoleColor.DarkRed);
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 도망치기");
            int input = cu.GetInput(0, 1);
            switch (input)
            {
                case 0:
                    MainScreen();
                    break;
                case 1:
                    AttackScreen(battle);
                    break;
            }


        }
        public void AttackScreen(Battle battle)
        {
            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
            Console.WriteLine();

            for (int i = 0; i < battle.battleMonsters.Count; i++)
            {
                ConsoleUtility.ColorWrite($"{i + 1} ", ConsoleColor.Blue);
                if (battle.battleMonsters[i].IsDead == true)
                {
                    ConsoleUtility.ColorWriteLine(battle.battleMonsters[i].MonsterDisplay(), ConsoleColor.DarkGray);
                }
                else
                {
                    Console.WriteLine(battle.battleMonsters[i].MonsterDisplay());
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
            Console.WriteLine($" {player.character} ({player.jobClass})");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{player.health}", ConsoleColor.DarkRed);
            Console.Write("/");
            ConsoleUtility.ColorWriteLine($"{player.maxHealth} ", ConsoleColor.DarkRed);
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            int input = cu.GetBattleInput(0, battle.battleMonsters.Count);
            if (input == 0)
            {
                BattleScreen(battle);
            }
            else if (input >= 1 && input <= battle.battleMonsters.Count)
            {
                if (battle.battleMonsters[input - 1].IsDead == true)
                {
                    Console.WriteLine("그 대상은 선택할 수 없습니다.");
                    Thread.Sleep(1000);
                    AttackScreen(battle);
                }
                else
                {
                    battle.PlayerAttack(input);

                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    Console.WriteLine();

                    int next = cu.GetInput(0, 0);

                    if (battle.isClear == 0)
                    {
                        battle.EnemyPhase();

                        Console.WriteLine();
                        Console.WriteLine("0. 다음");
                        Console.WriteLine();

                        if (battle.isClear == 0)
                        {
                            AttackScreen(battle);
                        }
                        else if (battle.isClear == 1)
                        {
                            BattleResultWin(battle);
                        }
                        else if (battle.isClear == 2)
                        {
                            BattleResultLose(battle);
                        }

                    }

                }
            }

        }
        public void BattleResultWin(Battle battle)
        {
            ConsoleUtility.Loading();
            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!! - Result", ConsoleColor.Yellow);
            Console.WriteLine();
            ConsoleUtility.ColorWriteLine("Victory", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {battle.battleMonsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
            Console.WriteLine($" {player.character} ({player.jobClass})");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{player.maxHealth}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            ConsoleUtility.ColorWrite($"{player.health}", ConsoleColor.DarkRed);
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
            int input = cu.GetBattleOverInput(0, 0);

        }

        public void BattleResultLose(Battle battle)
        {
            ConsoleUtility.Loading();
            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!! - Result", ConsoleColor.Yellow);
            Console.WriteLine();
            ConsoleUtility.ColorWriteLine("You Lose", ConsoleColor.Red);
            Console.WriteLine();
            ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
            Console.WriteLine($" {player.character} ({player.jobClass})");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{player.maxHealth}", ConsoleColor.DarkRed);      //player.maxhealth를 플레이어의 현재 체력 상황을 불러오는 식으로
            Console.Write(" -> ");
            ConsoleUtility.ColorWrite($"Dead", ConsoleColor.DarkRed);
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
            int input = cu.GetBattleOverInput(0, 0);
        }

        public void PotionScreen()
        {
            ConsoleUtility.Loading();
            Console.Clear();
            ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
            Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            int input = cu.GetInput(0, 1);
            switch (input)
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
            ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
            Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
            Console.WriteLine();

            if (item.IsUsed && item.Type == Item.ItemType.HpPotion) // 선택된 item.IsUsed가 True 이고 아이템의 타입이 HpPotion 이면
            {

                if (player.health < player.maxHealth)
                {
                    UseHealthPotion(itemList[0]);
                    itemList.Remove(item); // 사용 후 아이템 삭제
                    Console.Clear();
                    ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
                    Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
                    Console.WriteLine();
                }
                else if (player.health == player.maxHealth)
                {
                    Console.WriteLine($"체력이 가득찬 상태에서 {item.Name}을(를) 사용할 수 없습니다.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            int input = cu.GetInput(0, 1);
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



        public void QuestScreen()
        {
            Console.Clear();
            Console.WriteLine("[퀘스트 목록]");
            Console.WriteLine();

            // quest들에 대한 출력
            for (int i = 0; i < challenge.Count; i++)
            {
                Console.WriteLine(challenge[i].QuestDisplay());
            }

            Console.WriteLine("퀘스트를 선택하세요.");
            Console.WriteLine(">>  ");
            int input = cu.GetInput(1, 3);
            switch (input)
            {
                case 1:
                    MainScreen();
                    break;
                case 2:
                    UsePotion(itemList[0]);
                    break;
                case 3:

            }
        }

    }

<<<<<<< Updated upstream


=======
        // 아이템 사용 매서드 - 이전 사용매서드
        // public int UsePotion(Item item)
        // {
        //
        //     Console.Clear();
        //     ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
        //     Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
        //     Console.WriteLine();
        //
        //     if (item.IsUsed && item.Type == Item.ItemType.HpPotion) // 선택된 item.IsUsed가 True 이고 아이템의 타입이 HpPotion 이면
        //     {
        //
        //         if (player.health < player.maxHealth)
        //         {
        //             UseHealthPotion(itemList[0]);
        //             itemList.Remove(item); // 사용 후 아이템 삭제
        //             Console.Clear();
        //             ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
        //             Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
        //             Console.WriteLine();
        //         }
        //         else if (player.health == player.maxHealth)
        //         {
        //             Console.WriteLine($"체력이 가득찬 상태에서 {item.Name}을(를) 사용할 수 없습니다.");
        //             Console.WriteLine();
        //         }
        //     }
        //
        //     Console.WriteLine("1. 사용하기");
        //     Console.WriteLine("0. 나가기");
        //     int input = cu.GetInput(0, 1);
        //     switch (input)
        //     {
        //         case 0:
        //             PotionScreen();
        //             break;
        //         case 1:
        //             UsePotion(item);
        //
        //             break;
        //     }
        //     return player.health;
        //
        // }


        public void UseHealthPotion()
        {
            player.health = Math.Min(player.health, player.maxHealth);
        }


        public void QuestScreen()
        {
            quest.QuestStatus();
            int input = cu.GetInput(0, 0);
            MainScreen();
        }

        
>>>>>>> Stashed changes
    }
}