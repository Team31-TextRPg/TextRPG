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
                Console.WriteLine(">> ");
                if(int.TryParse(Console.ReadLine() , out int number) && number >=min && number <=max)
                {
                    return number;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }    
        }
    }
}
