using UnityEngine;
namespace Basic.Quests.Core
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

        protected virtual void FinishQuestStep(){ }

        protected virtual void ChangeState(string newState, string newStatus) { }

        protected abstract void SetQuestStepState(string state);
    }
}