namespace TextRPG;
{
    public class Main
    {
        // private Player player = new Player();
        //private PlayerInventory Playerinventory = new PlayerInventory();

        public void Game()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                switch (Console.ReadLine())
                {
                    case "1": Console.WriteLine("임의데이터"); break;
                    case "2": PlayerInventory.ShowPlayerInventory(); break;
                    case "3": Console.WriteLine("임의데이터"); break;
                    default: Console.WriteLine("잘못된 입력입니다."); break;
                }

                Console.ReadKey();
            }
        }
    }
    
    
    public class Item
    {
        public enum ItemType
        {
            Weapone,
            Amor,
            HpPotion
        }
    
    
        // 아이템 속성 : 이름. 회복력값. 
        // 아이템 기능 : 착용 가능. 플레이어 소유 가능.
        // 아이템 턴제 전투 중 먹을 수 있음. 회복아이템만 구현. 회복아이템은 사용시 소멸되며, HP 증가 구현.
        public string name;
        public ItemType type;
        public int value;
        public bool IsUsed;

        public Item (string name, string type, int value, bool isUsed = false)
        {
            new Item(name: "붉은 포션", type: "회복약", value: 10, isUsed: false);
            new Item(name: "붉은 포션", type: "회복약", value: 10, isUsed: false);
        }

        public string ItemDisplay() // 회복약 사용하기
        {
            // string used = IsUsed 
        }

        public string ItemDelete
        {
            if ()
            {
            bool IsUsed = false;
            }
            
        }

        public string UseItem()
        {
            string str = (Type == ItemType.HpPotion ? $"회복력 + {value}")
            bool IsUsed = true;
            return str;
        }
    
    }

    public class PlayerInventory
    {
        // 인벤토리는 플레이어가 보유 함. 플레이어는 한 명 임.
        // 아이템 획득 경로 : 몬스터에서 아이템 드랍이 있음. 드랍시 바로 인벤토리에 들어감 (줍기 여부 미구현)

        public string ShowPlayerInventory()
        {
            List<Item> items = new List<Item>();
            // 아이템이 여기에 추가 됨.
            
            
            public string itemDisplay = "";
            {
                // 아이템 사용시 아이템 플레이어 스텟에 영향이 가고 삭제. 아이템 사용은 ShowPlayerInventory에서 사용가능
            }
            
        }
        
        public string GetItem()
        {
            // 아이템 얻기
            //string str = ( == Item."회복약" 
        }

        
        
    }


}
