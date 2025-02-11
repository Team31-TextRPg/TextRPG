using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class QuestInfo
    {
        public string questName;
        public string questDescription;
        public string questGoal;
        public string monsterName;

        public Items rewardItem = null;   // 보상 아이템

        // int
        public int clearGold;      // 보상 골드
        public int currentCount = 0; // 현재 횟수
        public int maxCount = 10000;   // 최대 횟수
        public int atk = 1000;
        public int def = 1000;

        // bool
        public bool clearCheak = false; // 클리어 여부
        public bool access = false; // 수락 여부
        public bool rewardCheck = false; // 보상 획득 여부

        // 퀘스트 정보 보여주기
        public bool QuestShow(Charactor charactor, List<Items> shopitems, bool Restart)
        {
            // 퀘스트 클리어 여부 확인
            QuestClear(charactor);

            // 해당 퀘스트의 정보 출력
            Console.WriteLine("Quest");
            Console.WriteLine($"{questName}\n\n{questDescription}\n");
            Console.WriteLine($"- {questGoal}");

            Console.WriteLine("\n- 보상 -");
            // 보상 아이템이 있다면
            if (rewardItem != null)
            {
                Console.WriteLine(rewardItem.ItemName + " X 1 ");
            }
            Console.WriteLine(clearGold + "G\n");

            // 퀘스트 여부에 따른 텍스트 및 입력 변경

            // 1. 퀘스트를 클리어 했다면
            if (clearCheak && access)
            {
                Console.WriteLine("0. 돌아가기");
                Console.WriteLine("1. 보상 받기");
                // 선택에 따른 로직
                choice = base.Choice(1, true);
                if (choice == 0)
                {
                    Restart = true;
                }
                else if (choice == 1)
                {
                    Console.WriteLine("퀘스트 보상을 획득하셨습니다.");
                    rewardCheck = true;
                    // 클리어 골드 획득
                    charactor.Gold += clearGold;
                    // 아이템이 존재하면
                    if (rewardItem != null)
                    {
                        bool cheack = false; // 아이템 골드 변환 여부
                                             // 해당 아이템을 이미 습득했다면
                        foreach (Items item in charactor.Inven)
                        {
                            // 플레이어의 장비 목록에 보상 장비가 있다면)
                            if (item.ItemName == rewardItem.ItemName)
                            {
                                Console.WriteLine("해당 아이템을 이미 습득 하셨기 때문에 골드로 지급합니다.");
                                // 보상 장비의 상점 금액으로 지급
                                charactor.Gold += item.Price;
                                cheack = true;
                            }
                        }
                        // 아이템을 습득하지 않은 상태였다면 아이템을 획득
                        if (cheack == false)
                        {
                            foreach (Items items in shopitems)
                            {
                                if (items.ItemName == rewardItem.ItemName)
                                {
                                    // 플레이어 장비에 아이템 추가
                                    rewardItem.IsPurchase = true;
                                    charactor.Inven.Add(rewardItem);

                                    // 상점에서 해당 아이템 구매 완료로 변경
                                    items.IsPurchase = true;
                                }
                            }
                        }
                    }
                }
            }
            // 2. 퀘스트만 수락했다면
            else if (access)
            {
                Console.WriteLine("0. 돌아가기");
                // 선택에 따른 로직
                choice = base.Choice(0, true);
                Restart = true;
            }
            else
            {
                Console.WriteLine("0. 거절");
                Console.WriteLine("1. 수락");
                // 선택에 따른 로직
                choice = base.Choice(1, true);
                switch (choice)
                {
                    case 0:
                        Restart = true;
                        access = false;
                        break;
                    case 1:
                        // 몬스터의 이름이 
                        switch (monsterName)
                        {
                            case "Minion":
                                currentCount = charactor.MinionCount;
                                maxCount = currentCount + 5;
                                break;
                            case "CannonMinion":
                                currentCount = charactor.CannonCount;
                                maxCount = currentCount + 5;
                                break;
                            case "vacuity":
                                currentCount = charactor.vacuityCount;
                                maxCount = currentCount + 5;
                                break;
                            default:
                                // 몬스터 처치 퀘스트가 아니므로 없으므로 넘김
                                break;
                        }
                        Console.WriteLine($"퀘스트: {questName}를 수락하셨습니다.\n");
                        access = true;
                        break;
                }
            }
            return Restart;
        }

        // 퀘스트마다 클리어 조건을 각 클래스 별로 상속으로 설정함
        public virtual void QuestClear(Charactor _charactor)
        {
            // 임시 (퀘스트를 불러올 때 각 override가 실행이 되지 않아 같은 내용 추가함)
            switch (questName)
            {
                case "마을을 위협하는 미니언 처치":
                    // 최대 횟수가 0이 아니고 currentCount가 maxCount가 아니라면
                    if (maxCount <= currentCount)
                    {
                        clearCheak = true;
                    }
                    break;

                case "장비를 장착해보자":
                    if (_charactor.Weapon != null && _charactor.Armor != null)
                    {
                        if (_charactor.Weapon.IsEquiped && _charactor.Armor.IsEquiped)
                        {
                            clearCheak = true;
                        }
                        else
                        {
                            clearCheak = false;
                        }
                    }
                    else
                    {
                        clearCheak = false;
                    }
                    break;

                case "더욱 더 강해지기":
                    if (_charactor.Attack + _charactor.PlusAttack >= atk && _charactor.Defend + _charactor.PlusDefend >= def)
                    {
                        clearCheak = true;
                    }
                    else
                    {
                        clearCheak = false;
                    }
                    break;
            }
        }
    }

    // 사냥하기
    internal class Hunting : QuestInfo
    {
        public Hunting()
        {
            monsterName = "Minion";
            currentCount = 0;
            maxCount = 5;
            questName = "마을을 위협하는 미니언 처치";
            questDescription = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!";
            questGoal = string.Format($"{monsterName} 5마리 처치 ({currentCount} / {maxCount})");
            rewardItem = new Armor(); // Armor
            clearGold = 100;
        }

        public override void QuestClear(Charactor _charactor)
        {
            // 몬스터의 이름이 
            switch (monsterName)
            {
                case "Minion":
                    questGoal = string.Format($"{monsterName} 5마리 처치 ({_charactor.MinionCount - currentCount} / {maxCount - currentCount})");
                    break;
                case "CannonMinion":
                    questGoal = string.Format($"{monsterName} 5마리 처치 ({_charactor.MinionCount - currentCount} / {maxCount - currentCount})");
                    break;
                case "vacuity":
                    questGoal = string.Format($"{monsterName} 5마리 처치 ({_charactor.MinionCount - currentCount} / {maxCount - currentCount})");
                    break;
                default:
                    // 몬스터 처치 퀘스트가 아니므로 없으므로 넘김
                    break;
            }


            // 최대 횟수가 0이 아니고 currentCount가 maxCount가 아니라면
            if (maxCount <= currentCount)
            {
                clearCheak = true;
            }
        }

        internal class Quest
    {
        // 퀘스트 전체 정보
        public List<QuestInfo> QuestInfos = new List<QuestInfo>();
        // 퀘스트들의 정보
        Hunting hunt = new Hunting();
        EquipItem equip = new EquipItem();
        PowerUp power = new PowerUp();

        // 새로시작 시 퀘스트 생성
        public Quest(bool check)
        {
            QuestInfos.Add(hunt);
            QuestInfos.Add(equip);
            QuestInfos.Add(power);
        }
        // 이어하기 시 퀘스트 로드
        public Quest()
        {
            // 저장된 퀘스트의 오버로딩
        }
        // ShowQuest를 Quest클래스에서 사용하기 위해서
        public Quest(List<QuestInfo> quests)
        {
            QuestInfos = quests;
        }

        // 모든 퀘스트 보여주기
        public void ShowQusts(Charactor _charactor, List<Items> _items)
        {
            bool Restart = false;
            QuestInfos = RemoveQuest(QuestInfos);
            int questCount = QuestInfos.Count; // 현재 퀘스트 개수
            Console.WriteLine("Quest!!\n\n");
            int count = 1; // foreach 개수
            Console.WriteLine("0. 나가기");
            foreach (QuestInfo quest in QuestInfos)
            {
                // 수락 여부
                string accept = quest.access == true ? "O" : "X";
                Console.WriteLine($"{count}. [수락 {accept} ]{quest.questName}");
                count++;
            }
            Console.WriteLine("\n\n");
            choice = base.Choice(questCount, true);
            switch (choice)
            {
                case 1:
                    // 입력한 퀘스트 확인
                    Restart = QuestInfos[0].QuestShow(_charactor, _items, Restart);
                    break;
                case 2:
                    Restart = QuestInfos[1].QuestShow(_charactor, _items, Restart);
                    break;
                case 3:
                    QuestInfos[2].QuestClear(_charactor);
                    Restart = QuestInfos[2].QuestShow(_charactor, _items, Restart);
                    break;
            }
            // 돌아가기를 눌렀다면 재귀 함수 시작
            if (Restart)
            {
                ShowQusts(_charactor, _items);
            }
        }

        public List<QuestInfo> RemoveQuest(List<QuestInfo> _quests)
        {
            // 순서
            int count = 0;
            List<QuestInfo> newQuest = new List<QuestInfo>();
            foreach (QuestInfo quest in _quests)
            {
                // 보상을 못한 퀘스트만 추가
                if (!quest.rewardCheck)
                {
                    newQuest.Add(quest);
                }
                count++;
            }
            // 보상을 미획득한 퀘스트만 갱신
            return _quests = newQuest;
        }
    }
}


