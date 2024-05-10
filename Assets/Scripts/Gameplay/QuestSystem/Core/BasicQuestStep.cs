using UnityEngine;
namespace Basic.Quests.Core
{
    public abstract class BasicQuestStep : MonoBehaviour
    {
        protected bool isFinished = false;
        protected string questId;
        protected int stepIndex;

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