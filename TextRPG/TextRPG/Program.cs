using System.Collections.Generic;

namespace TextRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            Screen screen = new Screen();

            screen.MainScreen();
        }
    }

    //  게임이 시작될 때 필요한 모든 것들을 생성하는 클래스
    class GameManager
    {
        Player player;
        List<Monster> monsterList;
        //List<Item> itemList;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 100, 1500);
            monsterList = new List<Monster>
            {
                new Monster(2,"미니언",15,10),
                new Monster(3,"공허충", 10, 15),
                new Monster(5,"대포미니언",25,15)
            };

            //itemList = new List<Item>
            //{
            //    여러가지 아이템
            //};
        }
    }
}