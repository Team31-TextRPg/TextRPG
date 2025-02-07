namespace TextRPG;

public class PlayerInventory
{
    // 인벤토리 아이템 리스트
    public void ShowPlayerInventory()
    {
        Console.WriteLine("\n=== 인벤토리 ===");
        foreach (var item in items)
        {
            Console.WriteLine(item.ItemDisplay());
        }
    }
    
    // 아이템 사용하기
    public string UseItem()
    {
        if (Item.IsUsed == true && Item.ItemType == HpPotion) // 선택된 item.IsUsed가 True 이고 아이템의 타입이 HpPotion 이면
        {
            // 플레이어의 HP가 상승 , 회복효과 설명, 아이템이 삭제
            
            
            Console.WriteLine($"{item.Name} 사용! HP +{item.Description}"); // 회복 효과 설명
            items.Remove(item); // 사용 후 아이템 삭제
        }
        else
        {
            Console.WriteLine($"{item.Name}은(는) 사용할 수 없습니다.");
        }
    }
    
}