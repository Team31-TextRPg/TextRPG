using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    internal class Screen
    {
        
        //public Player(string name, string job, int level, int attackPower, int defensePower, int health, int gold)
      
        public void MainScreen()
        {
            ConsoleUtility cu = new ConsoleUtility();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("전투 시작");
            Console.WriteLine();
            int input = cu.GetInput(1, 2);
            switch (input)
            {
                case 1:
                    ShowStatusScreen(player);
                    break;
                case 2:
                    BattleScreen();
                    break;
            }

        }

        public void ShowStatusScreen(Player player)
        {
            player.ShowStatus();
            Console.WriteLine();

            int input = cu.GetInput(0, 0);
            MainScreen();
   
        }


        public void BattleScreen()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            //몬스터 

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{level} ({jobClass})");
            Console.WriteLine($"HP {health}/{maxHealth}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            int input = cu.GetInput(1, 1);
            BattleScreen();

        }

    }
}
