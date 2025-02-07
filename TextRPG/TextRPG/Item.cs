namespace TextRPG;

public class Item
{
    public enum ItemType
    {
        Weapon,
        Armor,
        HpPotion
    }
    
    public string Name {get;}
    public ItemType Type {get;}
    public int Value {get;}
    public string Description {get;}
    public bool IsUsed {get; set;}

    public Item (string name, string type, int value, string description, bool isUsed = false)
    {
        Name = name;
        Type = Type;
        Value = value;
        Description = description;
        IsUsed = false;
    }

    public string ItemDisplay() // 좀 이상함. 장착 관련 부분 차용했는데 
    {
        string str = IsUsed ? "[사용됨] " : "";
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
            _ => "알 수 없음"
        };
    }
    
}



