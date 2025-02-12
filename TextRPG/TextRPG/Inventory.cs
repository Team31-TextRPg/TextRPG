using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Inventory
    {
        // 플레이어가 소유한 아이템: 아이템 이름과 보유 개수를 관리하는 딕셔너리
        public Dictionary<string, int> itemQuantities;

        public Inventory()
        {
            itemQuantities = new Dictionary<string, int>();
        
            // 기본 아이템을 미리 추가
            AddItem("HealthPotion", 5);
            AddItem("SlimeArmor", 1);
        }

        // 아이템 추가: 이미 있으면 수량 증가, 없으면 새 항목 생성
        public void AddItem(string itemName, int quantity = 1)
        {
            if (itemQuantities.ContainsKey(itemName))
            {
                itemQuantities[itemName] += quantity;
            }
            else
            {
                itemQuantities[itemName] = quantity;
            }
        }

        // 아이템 사용: 보유 수량이 있으면 1 감소시키고, 0이 되어도 항목은 남김 (단, 화면 출력 시 건너뜀)
        public bool UseItem(string itemName)
        {
            if (itemQuantities.ContainsKey(itemName))
            {
                if (itemQuantities[itemName] > 0)
                {
                    itemQuantities[itemName]--;
                    Console.WriteLine($"{itemName} 사용됨. 남은 개수: {itemQuantities[itemName]}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{itemName}의 보유 개수가 0입니다.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"{itemName}이(가) 인벤토리에 없습니다.");
                return false;
            }
        }

        // 보유 수량이 0보다 큰 아이템 이름 목록 반환
        public List<string> GetAvailableItems()
        {
            List<string> available = new List<string>();
            foreach (var pair in itemQuantities)
            {
                if (pair.Value > 0)
                    available.Add(pair.Key);
            }
            return available;
        }

        // 특정 아이템의 보유 수량 반환
        public int GetItemQuantity(string itemName)
        {
            if (itemQuantities.ContainsKey(itemName))
                return itemQuantities[itemName];
            return 0;
        }

        // 인벤토리 목록 출력: 보유 수량이 0인 항목은 화면에 표시하지 않음
        public void DisplayInventory()
        {
            List<string> available = GetAvailableItems();
            
            if (available.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            int index = 1;
            foreach (var itemName in available)
            {
                // 아이템 도감에서 아이템 세부 정보를 가져옴
                Item item = ItemDatabase.GetItem(itemName);
                string display = item != null ? item.ItemDisplay() : itemName;
                int quantity = GetItemQuantity(itemName);
                Console.WriteLine($"{index}. {display} (x{quantity})");
                index++;
            }
        }

        //  Inventory 클래스의 데이터를 저장하는 함수
        public void Save(string filePath)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        //  Inventory 클래스의 데이터를 불러오는 함수
        public static Inventory Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Inventory>(json);
        }
    }
}