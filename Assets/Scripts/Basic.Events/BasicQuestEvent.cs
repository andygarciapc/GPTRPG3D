using System;
namespace Basic.Events
{
    public class BasicQuestEvent
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

        public event Action onQuestStateChange;
        public void QuestStateChange()//BasicQuest quest)
        {
            if (onQuestStateChange != null)
            {
                onQuestStateChange();//quest);
            }
        }

        public event Action<string, int, string, string> onQuestStepStateChange;
        public  void QuestStepStateChange(string id, int stepIndex, string state, string status)
        {
            if (onQuestStepStateChange != null)
            {
                onQuestStepStateChange(id, stepIndex, state, status);
            }
        }
    }
}