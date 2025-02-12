using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    internal class Quest
    {
        // string
        public string QuestId { get; set; }   // 퀘스트 이름
        public string QuestContent { get; set; }   // 퀘스트 설명
        public string QuestGoal { get; set; }   // 목표
        public string MonsterName { get; set; } // 목표 몬스터 이름
        public string QuestReward { get; set; }
        public string QuestAcceptMessage { get; set; }


         // 보상 아이템
        public int QuestGold { get; set; }  // 보상 골드
        public int CurrentCount { get; set; }   // 현재 횟수
        public int MaxCount { get; set; }  // 최대 횟수
        public int Atk { get; set; } = 1000;
        public int Def { get; set; } = 1000;

        // bool
        public bool clearCheck { get; set; } = false;  // 클리어여부
        public bool access { get; set; } = false; // 수락 여부
        public bool rewardCheck { get; set; } = false; // 보상 획득 여부
        public bool questAccept { get; set; } = false;

        public Quest(string accepted, string questId, string questContent, string questReward, int questGold)
        {
            QuestAcceptMessage = accepted;
            QuestId = questId;
            QuestContent = questContent;
            QuestReward = questReward;
            QuestGold = questGold;
        }

        public void Acceppt()
        {
            if (questAccept == false)
            {
                QuestAcceptMessage = "";
            }
            else
            {
                QuestAcceptMessage = " | 수락한 퀘스트 ";

            }

        }

    }
}

