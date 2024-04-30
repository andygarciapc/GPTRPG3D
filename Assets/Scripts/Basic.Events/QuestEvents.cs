using System;
using Basic.Quests.Variable;
using Basic.Quests.Core;
namespace Basic.Events
{
    public class QuestEvents
    {
        public event Action<string> onStartQuest;
        public void StartQuest(string id)
        {
            if (onStartQuest != null)
            {
                onStartQuest(id);
            }
        }

        public event Action<string> onAdvanceQuest;
        public void AdvanceQuest(string id)

        {
            if (onAdvanceQuest != null)
            {
                onAdvanceQuest(id);
            }
        }

        public event Action<string> onFinishQuest;
        public void FinishQuest(string id)
        {
            if (onFinishQuest != null)
            {
                onFinishQuest(id);
            }
        }

        public event Action<BasicQuest> onQuestStateChange;
        public void QuestStateChange(BasicQuest quest)
        {
            if (onQuestStateChange != null)
            {
                onQuestStateChange(quest);
            }
        }

        public event Action<string, int, BasicQuestStepState> onQuestStepStateChange;
        public  void QuestStepStateChange(string id, int stepIndex, BasicQuestStepState questStepState)
        {
            if (onQuestStepStateChange != null)
            {
                onQuestStepStateChange(id, stepIndex, questStepState);
            }
        }
    }
}