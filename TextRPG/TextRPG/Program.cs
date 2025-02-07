using System.Collections.Generic;

namespace TextRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();

            gm.MainScreen();
        }
    }

    //  게임이 시작될 때 필요한 모든 것들을 생성하는 클래스
    public class GameManager
    {
        Player player;
        List<Monster> monsterList;
        ConsoleUtility cu;
        //List<Item> itemList;

        public GameManager()
        {
            player = new Player("Chad", "전사", 1, 10, 5, 100, 1500);
            cu = new ConsoleUtility();

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

        public void MainScreen()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("전투 시작");
            Console.WriteLine();

            int input = cu.GetInput(1, 2);
            switch (input)
            {
                case 1: //상태 보기 함수 호출
                    break;
                case 2: //전투 시작 함수 호출
                    break;
            }

        }
        public void StatusScreen()
        {


        }

        public void BattleScreen()
        {

        }
    }
}