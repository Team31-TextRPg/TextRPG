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
        Battle battle;
        Player player;
        Inventory inventory;
        List<Monster> monsterList;

        ConsoleUtility cu;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 80, 100, 80, 100, 1500);
            inventory = new Inventory();
            cu = new ConsoleUtility();

            monsterList = new List<Monster>
            {
                new Monster(1,"미니언",15, 3),
                new Monster(2,"공허충", 10, 5),
                new Monster(3,"대포미니언",25, 4),
                new Monster(4,"칼날부리",30,5),
                new Monster(5,"늑대",20,7)
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
                    player.Thief();
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
            Console.WriteLine($"2. 전투 시작 (현재 진행 : {player.floor}층)");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 저장하기");
            Console.WriteLine("5. 불러오기");
            Console.WriteLine();

            int input = cu.GetInput(1, 5);
            switch (input)
            {
                case 1:
                    StatusScreen();
                    break;
                case 2:
                    battle = new Battle(player, monsterList, player.floor);
                    BattleScreen(battle);
                    break;
                case 3:
                    PlayerInventoryScreen(inventory);
                    break;
                case 4:
                    SaveScreen(player);
                    break;
                case 5:
                    LoadScreen(ref player, ref inventory);
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
            ConsoleUtility.ColorWrite($"{player.mana}", ConsoleColor.DarkRed);
            Console.Write("/");
            ConsoleUtility.ColorWriteLine($"{player.maxMana} ", ConsoleColor.DarkRed);
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
                if (player.mana < player.skillList[input - 1].mpValue)
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
                    if (player.exp >= player.maxExp)
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
            ConsoleUtility.ColorWrite($"{player.mana}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            player.mana += 10;
            if(player.mana > player.maxMana)
            {
                player.mana = player.maxMana;
            }
            ConsoleUtility.ColorWriteLine($"{player.mana}", ConsoleColor.DarkRed);
            Console.Write("Exp ");
            ConsoleUtility.ColorWrite($"{curExp}", ConsoleColor.DarkRed);
            Console.Write(" -> ");
            ConsoleUtility.ColorWriteLine($"{player.exp}", ConsoleColor.DarkRed);
            Console.WriteLine();

            player.floor++;

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
        

        public void PlayerInventoryScreen(Inventory inventory)
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
                
                switch (selectedItem.Type)
                {
                    case Item.ItemType.HpPotion:
                        if (player.health < player.maxHealth)
                        {
                            UseHealthPotion(selectedItem);
                            inventory.UseItem(selectedItemName);
                        }
                        else
                        {
                            Console.WriteLine("체력이 가득찬 상태에서 사용할 수 없습니다.");
                        }
                        break;
                    
                    case Item.ItemType.ManaPotion:
                        if (player.mana < player.maxMana)
                        {
                            UseManaPotion(selectedItem);
                            inventory.UseItem(selectedItemName);
                        }
                        else
                        {
                            Console.WriteLine("마력이 가득찬 상태에서 사용할 수 없습니다.");
                        }
                        break;
	    
                    case Item.ItemType.Armor:
                        if (selectedItem.IsUsed)
                        {
                            Console.WriteLine($"{selectedItem.Name} 장착을 해제합니다.");
                            selectedItem.IsUsed = false;
                        }
                        else
                        {
                            EquipArmor(selectedItem);
                            Console.WriteLine($"{selectedItem.Name} 장착을 장착합니다.");
                            selectedItem.IsUsed = true;
                        }
                        break;

                    
                    default:
                        Console.WriteLine("사용할 수 없는 아이템입니다.");
                        break;       
                }        
                // 아이템 사용 후 새로고침
                Thread.Sleep(1000);
                PlayerInventoryScreen(inventory); 
                
            }

        }

        public void UseHealthPotion(Item selectedItem)
        {
            player.health = Math.Min(player.health + selectedItem.Value, player.maxHealth);
        }
        
        public void UseManaPotion(Item selectedItem)
        {
            player.mana = Math.Min(player.mana + selectedItem.Value, player.maxMana);
        }
        
        public void EquipArmor(Item selectedItem)
        {
            player.defense = (player.defense + selectedItem.Value);
        }

        //  저장하기 장면 함수
        public void SaveScreen(Player player)
        {
            Console.Clear();

            Console.WriteLine("저장하기");
            Console.WriteLine("현재까지 플레이한 데이터를 저장할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = cu.GetInput(0, 1);
            switch (input)
            {
                case 0:
                    MainScreen();
                    break;
                case 1:
                    DataSave(player);
                    Console.WriteLine("데이터를 저장했습니다.");
                    Thread.Sleep(1000);
                    MainScreen();
                    break;
            }
        }

        public void DataSave(Player player)
        {
            player.Save("player.json");
            inventory.Save("Inventory.json");
        }

        //  불러오기 장면 함수
        public void LoadScreen(ref Player player, ref Inventory inventory)
        {
            Console.Clear();

            Console.WriteLine("불러오기");
            Console.WriteLine("이전에 저장했던 데이터를 불러올 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 불러오기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = cu.GetInput(0, 1);
            switch (input)
            {
                case 0:
                    MainScreen();
                    break;
                case 1:
                    DataLoad(ref player, ref inventory);
                    Console.WriteLine("데이터를 불러왔습니다.");
                    Thread.Sleep(1000);
                    MainScreen();
                    break;
            }
        }
        public void DataLoad(ref Player player, ref Inventory inventory)
        {
            player = Player.Load("player.json");
            inventory = Inventory.Load("inventory.json");
        }
    }
}