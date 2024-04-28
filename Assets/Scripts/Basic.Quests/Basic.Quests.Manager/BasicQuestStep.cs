using UnityEngine;
using Basic.Events;
namespace Basic.Quests
{
    public abstract class BasicQuestStep : MonoBehaviour
    {
        private bool isFinished = false;
        private string questId;
        private int stepIndex;

        public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
        {
            this.questId = questId;
            this.stepIndex = stepIndex;
            if (questStepState != null && questStepState != "")
            {
                SetQuestStepState(questStepState);
            }
        }

        protected void FinishQuestStep()
        {
            if (!isFinished)
            {
                isFinished = true;
                BasicEventsManager.instance.questEvents.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
        }

        protected void ChangeState(string newState, string newStatus)
        {
            BasicEventsManager.instance.questEvents.QuestStepStateChange(
                questId,
                stepIndex,
                newState, newStatus
            );
        }

        protected abstract void SetQuestStepState(string state);
    }
}