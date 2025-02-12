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
        Inventory inventory;
        Item item;
        int floor;
        Quest quest;

        List<Monster> monsterList;
        public List<Item> itemList;
        List<Quest> questsList;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 80, 100, 1500);
            cu = new ConsoleUtility();
            floor = 1;

            monsterList = new List<Monster>
            {
                new Monster(1,"미니언",15, 3),
                new Monster(2,"공허충", 10, 5),
                new Monster(3,"대포미니언",25, 4),
                new Monster(4,"칼날부리",30,5),
                new Monster(5,"늑대",20,7)
            };

            questsList = new List<Quest>
            {
                new Quest("","1. 작고 하찮은 미니언 처치","작고 하찮은 미니언들이 너무 많아졌다고 생각하지 않나?\n저놈들을 처치해버려!", "미니언 방패", 5),
                new Quest("","2. 마음을 엄습하는 공허충 처치","추운 겨울에도 공허충은 내 옆구리를 시리게 할 수 없지!\n저놈들을 처치해버려!", "공허의 칼날", 5),
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
                    battle = new Battle(player, monsterList, floor);
                    BattleScreen(battle);
                    break;
                case 3:
                    inventory = new Inventory();
                    PlayerInventoryScreen(inventory, item);
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
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 도망치기");
            int input = cu.GetInput(0, 2);
            switch (input)
            {
                case 0:
                    MainScreen();
                    break;
                case 1:
                    AttackScreen(battle);
                    break;
                case 2:
                    SkillScreen(battle);
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
                            BattleScreen(battle);
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

        //  무슨 스킬을 사용할 지 고르는 화면
        public void SkillScreen(Battle battle)
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
            Console.Write("MP ");
            ConsoleUtility.ColorWrite($"{player.mp}", ConsoleColor.DarkRed);
            Console.Write("/");
            ConsoleUtility.ColorWriteLine($"{player.maxMp} ", ConsoleColor.DarkRed);
            Console.WriteLine();

            for (int i = 0; i < player.skillList.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                player.skillList[i].SkillInfo();
            }

            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            int input = cu.GetInput(0, player.skillList.Count);
            if (input == 0)
            {
                BattleScreen(battle);
            }
            else if (input >= 1 && input <= player.skillList.Count)
            {
                if (player.mp < player.skillList[input - 1].mpValue)
                {
                    Console.WriteLine("Mp가 부족합니다.");
                    Thread.Sleep(1000);
                    SkillScreen(battle);
                }
                else
                {
                    if (player.skillList[input - 1].isRandom)
                    {
                        SkillAttackRandom(battle, input);
                    }
                    else
                    {
                        SkillAttackSelect(battle, input);
                    }
                }
            }

        }

        //  랜덤한 적을 공격하는 스킬을 사용했을 때 나오는 화면
        public void SkillAttackRandom(Battle battle, int skillIndex)
        {
            player.skillList[skillIndex - 1].Use(player, battle, -999);
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            cu.GetInput(0, 0);

            if (battle.isClear == 0)
            {
                battle.EnemyPhase();

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();

                if (battle.isClear == 0)
                {
                    BattleScreen(battle);
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
            else if (battle.isClear == 1)
            {
                BattleResultWin(battle);
            }
            else if (battle.isClear == 2)
            {
                BattleResultLose(battle);
            }
        }

        //  적을 지정해서 공격하는 스킬을 사용했을 때 나오는 화면
        public void SkillAttackSelect(Battle battle, int skillIndex)
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
                    player.skillList[skillIndex - 1].Use(player, battle, input);

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
                            BattleScreen(battle);
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
            Console.WriteLine("[캐릭터 정보]");
            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{player.level}", ConsoleColor.DarkRed);
            Console.Write(" -> ");

            int curExp = player.exp;
            int sumMonsterLevel = 0;

            for (int i = 0; i < battle.battleMonsters.Count; i++)
            {
                sumMonsterLevel += battle.battleMonsters[i].Level;
            }

            player.exp += sumMonsterLevel;
            switch (player.level)
            {
                case 1:
                    if(player.exp >= player.maxExp)
                    {
                        player.level++;
                        player.exp = player.exp - player.maxExp;
                        player.maxExp = 35;

                        player.attack += 0.5f;
                        player.defense += 1;
                    }
                    break;
                case 2:
                    if (player.exp >= player.maxExp)
                    {
                        player.level++;
                        player.exp = player.exp - player.maxExp;
                        player.maxExp = 65;

                        player.attack += 0.5f;
                        player.defense += 1;
                    }
                    break;
                case 3:
                    if (player.exp >= player.maxExp)
                    {
                        player.level++;
                        player.exp = player.exp - player.maxExp;
                        player.maxExp = 100;

                        player.attack += 0.5f;
                        player.defense += 1;
                    }
                    break;
                case 4:
                    if (player.exp >= player.maxExp)
                    {
                        player.level++;
                        player.exp = player.exp - player.maxExp;
                        player.maxExp = 9999;

                        player.attack += 0.5f;
                        player.defense += 1;
                    }
                    break;
                case 5:
                    if (player.exp >= player.maxExp)
                    {
                        player.level++;
                        player.exp = player.exp - player.maxExp;
                        player.maxExp = 9999;

                        player.attack += 0.5f;
                        player.defense += 1;
                    }
                    break;
            }

            Console.Write("Lv.");
            ConsoleUtility.ColorWrite($"{player.level} ", ConsoleColor.DarkRed);
            Console.WriteLine($" {player.character} ({player.jobClass})");
            Console.Write("HP ");
            ConsoleUtility.ColorWrite($"{player.maxHealth}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            ConsoleUtility.ColorWriteLine($"{player.health}", ConsoleColor.DarkRed);
            Console.Write("MP ");
            ConsoleUtility.ColorWrite($"{player.mp}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            player.mp += 10;
            if(player.mp > player.maxMp)
            {
                player.mp = player.maxMp;
            }
            ConsoleUtility.ColorWriteLine($"{player.mp}", ConsoleColor.DarkRed);
            Console.Write("Exp ");
            ConsoleUtility.ColorWrite($"{curExp}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            ConsoleUtility.ColorWriteLine($"{player.exp}", ConsoleColor.DarkRed);
            Console.WriteLine();

            floor++;

            Console.WriteLine("[획득 아이템]");
            int rewardGold = sumMonsterLevel * 70;
            Console.WriteLine($"{rewardGold} Gold");
            player.gold += rewardGold;
            // + 여러 장비나 포션들


            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = cu.GetBattleOverInput(0, 0);

            MainScreen();
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

        // public void PotionScreen() // 포션만 사용 스크린 (작업중)
        // {
        //     ConsoleUtility.Loading();
        //     Console.Clear();
        //     ConsoleUtility.ColorWriteLine("회복", ConsoleColor.Cyan);
        //     Console.WriteLine($"포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : {itemList.Count})");
        //     Console.WriteLine();
        //     Console.WriteLine("1. 사용하기");
        //     Console.WriteLine("0. 나가기");
        //     int input = cu.GetInput(0, 1);
        //     switch (input)
        //     {
        //         case 0:
        //             MainScreen();
        //             break;
        //         case 1:
        //             UsePotion(itemList[0]);
        //             break;
        //     }
        // }



        public void PlayerInventoryScreen(Inventory inventory, Item item)
        {
            Console.Clear();
            ConsoleUtility.ColorWriteLine("인벤토리", ConsoleColor.Cyan);
            Console.WriteLine();
            inventory.DisplayInventory();
            
            // 사용 가능한 아이템 목록을 가져옴
            List<string> available = inventory.GetAvailableItems();

            Console.WriteLine("\n아이템을 사용하려면 번호를 입력하세요.");
            Console.WriteLine("0. 메인화면으로가기");
            int input = cu.GetInput(0, available.Count);

            if (input == 0)
            {
                MainScreen();
            }
            else  
            {
                string selectedItemName = available[input - 1];
                Item selectedItem = ItemDatabase.GetItem(selectedItemName);
                
                if (selectedItem.Type == Item.ItemType.HpPotion)
                {
                    if (player.health < player.maxHealth)
                    {
                        UseHealthPotion(); // 플레이어 스텟 변경
                        inventory.UseItem(selectedItemName);
                        
                    }
                    else if (player.health == player.maxHealth)
                    {
                        Console.WriteLine("체력이 가득찬 상태에서 사용할 수 없습니다.");
                    }
					          
                }
			          
                else
                {
                    Console.WriteLine("사용할 수 없는 아이템 입니다.");
                }

                // 아이템 사용 후 새로고침
                Thread.Sleep(1000);
                PlayerInventoryScreen(inventory, item); 
                
            }
            
        }




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
            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine(questsList[0].QuestId + questsList[0].QuestAcceptMessage);
            Console.WriteLine(questsList[1].QuestId + questsList[1].QuestAcceptMessage);
            Console.WriteLine();
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>   ");
            int questSelect = cu.GetInput(1, 2);
            QuestSubScreen(questSelect);
        }

        public void QuestSubScreen(int questSelect)
        {
            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine(questsList[questSelect - 1].QuestId);
            Console.WriteLine(questsList[questSelect - 1].QuestContent);
            Console.WriteLine();
            Console.WriteLine($"-5마리 처치");
            Console.WriteLine("\n- 보상 -");
            Console.WriteLine(questsList[questSelect - 1].QuestReward + " X 1");
            Console.WriteLine(questsList[questSelect - 1].QuestGold + "G\n");
            Console.WriteLine("0.돌아가기");
            Console.WriteLine("1. 수락");
            int questOK = cu.GetInput(0, 1);
            switch (questOK)
            {
                case 0:
                    QuestScreen();
                    break;
                case 1:
                    Console.WriteLine();
                    Console.Write("퀘스트를 수락하셨습니다.");
                    questsList[questSelect - 1].questAccept = true;
                    questsList[questSelect - 1].Acceppt();
                    QuestScreen();
                    break;
            }


        }

    }
}