using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextRPG
{
    public class ConsoleUtility
    {
        public int GetInput(int min, int max)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine();
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public int GetBattleInput(int min, int max)           //배틀 중에 입력하는 값을 리턴해주는 함수
        {
            while (true)
            {

                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine();
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        public int GetBattleOverInput(int min, int max)       //배틀에서 이겼을 때나 졌을 때 입력하는 값을 리턴해주는 함수
        {
            while (true)
            {

                if (int.TryParse(Console.ReadLine(), out int number) && number >= min && number <= max)
                {
                    return number;
                }
                Console.WriteLine();
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
    }
}

