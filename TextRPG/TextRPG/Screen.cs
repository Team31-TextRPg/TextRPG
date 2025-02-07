using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Screen
    {
        ConsoleUtility cu = new ConsoleUtility();
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
            //Console.WriteLine("상태 보기");
            //Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            //Console.WriteLine();
            //Console.WriteLine($"Lv. {Level("00")}");
            //Console.WriteLine($"{Name} ( {Job} )";
            //Console.WriteLine($"공격력 : {Atk}");
            //Console.WriteLine($"방어력 : {Def}");
            //Console.WriteLine($"체  력 : {Hp}");
            //Console.WriteLine($"Gold : {Gold}");
            //Console.WriteLine("1");
            //Console.WriteLine("0. 나가기");

        }

        public void BattleScreen()
        {

        }

    }
}
