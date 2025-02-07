namespace TextRPG;

class Program
{
    static void Main(string[] args)
    {
        GameManager gm = new GameManager();
        gm.PlayerInventoryScreen();
    }
}


class GameManager
{
    Player player;
    List<Item> itemList;
    
    public GameManager()
    {
        //Player(string name, string job, int level, int attackPower, int defensePower, int health, int gold)
        player = new Player("플레이어", "전사", 1, 100, 100, 100, 4000);

        //Item(string name, string type, int value, string description, bool isUsed = false)
        itemList = new List<Item>
        {
            new Item(name: "작은 붉은 포션", type: Item.ItemType.HpPotion, value: 10, description: "10 HP 회복됩니다.", isUsed: true),
            new Item(name: "큰 붉은 포션", type: Item.ItemType.HpPotion, value: 20, description: "20 HP 회복됩니다.", isUsed: false)
        };
    }
    
    
    // 인벤토리 화면
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




