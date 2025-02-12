namespace TextRPG;

public class Item
{
    public enum ItemType
    {
        Weapon,
        Armor,
        HpPotion,
        ManaPotion,
    }
    
    public string Name {get;}
    public ItemType Type {get;}
    public int Value {get;}
    public string Description {get;}
    public bool IsUsed {get; set;}

    public Item (string name, ItemType type, int value, string description, bool isUsed = false) //아이템 생성자
    {
        Name = name;
        Type = type;
        Value = value;
        Description = description;
        IsUsed = isUsed;
    }

    public string ItemDisplay() // 좀 이상함. 장착 관련 부분 차용했는데 
    {
        string str = IsUsed ? "[장착됨] " : "";
        str += $"{Name} | {GetTypeString()} | {Description}";
        return str; 
    }
    
    public string GetTypeString()
    {
        return Type switch
        {
            ItemType.Weapon => "무기",
            ItemType.Armor => "방어구",
            ItemType.HpPotion => "회복약",
            ItemType.ManaPotion => "회복약"
        };
    }
    
}




