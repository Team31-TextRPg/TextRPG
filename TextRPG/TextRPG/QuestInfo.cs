using System;
using TextRPG;

internal class QuestInfo
{
    public string questName { get; set; }
    public string questDescription { get; set; }
    public string questGoal { get; set; }
    public string monsterName { get; set; }

    public Item rewardItem { get; set; } = null;   // 보상 아이템

    public int clearGold { get; set; } = 5;      // 보상 골드
    public int currentCount { get; set; } = 0; // 현재 횟수
    public int maxCount { get; set; } = 1000;   // 최대 횟수
    public int atk { get; set; } = 1000;
    public int def { get; set; } = 1000;

    // bool
    public bool clearQuest { get; set; } = false; // 클리어 여부
    public bool access { get; set; } = false; // 수락 여부
    public bool rewardCheck { get; set; } = false; // 보상 획득 여부

    public void QuestClear()
    {
        if (maxCount > 0 && maxCount <= currentCount)
        {
            clearQuest = true;
        }
        else
        {
            clearQuest = false;
        }
    }

    public bool QuestDisplay(Charactor charactor, List<Items> shopitems, bool Restart)
    {
        QuestClear(charactor);
        Console.WriteLine("Quest");
        Console.WriteLine($"{questName}\n\n{questDescription}\n");
        Console.WriteLine($"- {questGoal}");

        Console.WriteLine("\n- 보상 -");

        if (rewardItem != null)
        {
            Console.WriteLine(rewardItem.ItemName + " X 1 ");
        }
        Console.WriteLine(clearGold + "G\n");

        // 퀘스트 여부에 따른 텍스트 및 입력 변경

        // 1. 퀘스트를 클리어 했다면
        if (clearQuest && access)
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
        rewardItem = new Shield();
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
            clearQuest = true;
        }
    }
}

