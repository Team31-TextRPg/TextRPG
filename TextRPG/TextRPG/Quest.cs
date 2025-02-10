using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Quest
    {
        //questList 리스트
        static List<Quest> questList = new List<Quest>();

        //quest 생성시 들어갈 내용
        public string questName { get; private set; }
        public string questReward {  get; private set; }

        //quest 생성자
        public Quest()
        {

        }
        //showQuestMenu 메서드 아직 수락하지 않은 퀘스트 목록을 보여줌.
        public void showQuestMenu()
        {
            Console.Clear();
            Console.WriteLine("Quest");
            Console.WriteLine();
            Console.WriteLine();
        }

        //quest 내용 확인 메서드.
        public void detailQuest()
        {

        }
    }
}
