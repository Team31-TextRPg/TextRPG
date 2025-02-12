using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Quest
    {
        public int QuestId { get; set; }
        public string QuestContent { get; set; }
        public int QuestGoal { get; set; }
        public int UnderWay { get; set; }
        public string QuestReward { get; set; }
        public int QuestGold { get; set; }
        public bool IsSuccess { get; set; }

        public Quest(int questId, string questContent, int questGoal, int underWay, string questReward, int questGold, bool isSuccess)
        {
            QuestId = questId;
            QuestContent = questContent;
            QuestGoal = questGoal;
            UnderWay = underWay;
            QuestReward = questReward;
            QuestGold = questGold;
            IsSuccess = isSuccess;
        }
    }
}
