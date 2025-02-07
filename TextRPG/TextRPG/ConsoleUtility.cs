using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class ConsoleUtility
    {
        public int GetInput(int min, int max)
        {
            while(true)
            {
                
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if(int.TryParse(Console.ReadLine() , out int number) && number >=min && number <=max)
                {
                    return number;
                }
                Console.WriteLine();
                Console.WriteLine("잘못된 입력입니다.");
            }    
        }

        public int GetBattleInput(int min, int max)
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
        public int GetBattleOverInput(int min, int max)
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
