using System;
using System.Collections.Generic;

namespace TextRPG
{
    public static class ItemDatabase
    {
        // 아이템 이름을 키로, Item 객체를 값으로 하는 딕셔너리
        private static Dictionary<string, Item> items;

        static ItemDatabase()
        {
            items = new Dictionary<string, Item>();
            // 게임에 등장하는 모든 아이템 등록
            items.Add("HealthPotion", new Item("붉은 포션", Item.ItemType.HpPotion, 30, "사용시 체력을 30 회복합니다."));
            items.Add("OugerSword", new Item("오우거의 칼", Item.ItemType.Weapon, 150, "작지만 강력한 무기입니다. 착용시 공격력이 150 추가됩니다."));
            items.Add("SlimeArmor", new Item("슬라임 전신 갑옷", Item.ItemType.Armor, 120, "적의 칼날이 미끄러집니다. 착용시 방어력이 120 추가됩니다."));
            // >>필요에 따라 추가 아이템 등록<<
            items.Add("ManaPotion", new Item("파란 포션", Item.ItemType.ManaPotion, 30, "사용시 마나를 30 회복합니다."));

        }

        // 아이템 이름으로 아이템 정보를 반환
        public static Item GetItem(string name)
        {
            if (items.ContainsKey(name))
                return items[name];
            return null;
        }
    }
}