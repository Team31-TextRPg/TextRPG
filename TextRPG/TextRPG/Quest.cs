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
        public string Name { get; private set; }  // 퀘스트 이름
        public string Description { get; private set; }  // 퀘스트 설명
        public int RequiredKills { get; private set; }  // 처치해야 할 몬스터 수
        public int CurrentKills { get; private set; }  // 현재 처치한 몬스터 수
        public string RewardItem { get; private set; }  // 보상 아이템
        public int RewardGold { get; private set; }  // 보상 골드
        public bool IsCompleted => CurrentKills >= RequiredKills;  // 완료 여부

        List<Quest> questList = new List<Quest>(); //퀘스트 리스트

        //퀘스트 생성자
        public Quest(string name, string description, int requiredKills, string rewardItem, int rewardGold)
        {
            Name = name;
            Description = description;
            RequiredKills = requiredKills;
            RewardItem = rewardItem;
            RewardGold = rewardGold;
            CurrentKills = 0;
        }
        //퀘스트 내용
        public void ProgressQuest()
        {

        }

        //showQuestMenu 메서드 아직 수락하지 않은 퀘스트 목록을 보여줌.
        public void showQuestMenu()
        {
            Console.Clear();
            Console.WriteLine("Quest!!\n");

            for (int i = 0; i < questList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questList[i].Name}");
            }

            Console.Write("\n원하시는 퀘스트를 선택해주세요: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= questList.Count)
            {
                ShowQuestDetails(questList[choice - 1]);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        //퀘스트 상세 정보 확인
        static void ShowQuestDetails(Quest quest)
        {

        }
    }
}
