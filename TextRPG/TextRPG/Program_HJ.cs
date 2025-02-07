namespace TextRPG;

class GameManager
{
    public GameManager()
    {
        //Player(int level, string name, int atk, int def, int maxHp, int gold)
        player = new Player(1, "Chad", 10, 5, 150, 10000);

        //Item(string name, ItemType type, int value, string descrip, int cost)
        itemList = new List<Item>
        {
            new Item(name: "작은 붉은 포션", type: Item.ItemType.HpPotion, value: 10, description: "10 HP 회복됩니다.", isUsed: false);
            new Item(name: "큰 붉은 포션", type: Item.ItemType.HpPotion, value: 20, description: "20 HP 회복됩니다.", isUsed: false);
        };
    }
    
    
    private playerInventory = new PlayerInventory(); // 인벤토리 선언
    
    // 인벤토리 화면
    public void PlayerInventoryScreen()
    {
        Console.WriteLine("[나의 인벤토리]");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
         
        // 인벤토리에 있는 아이템 출력
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            Console.WriteLine(PlayerInventory[i].ItemDisplay()); 
        }
    }
}




