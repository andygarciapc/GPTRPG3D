using Basic.Events;
using Basic.Quests.Core;
namespace Basic.Quests.Manager
{
    public abstract class QuestStep : BasicQuestStep
    {
        protected override void FinishQuestStep()
        {
            if (!isFinished)
            {
                isFinished = true;
                GameEventsManager.instance.questEvents.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
        }

        protected override void ChangeState(string newState, string newStatus)
        {
            GameEventsManager.instance.questEvents.QuestStepStateChange(
                questId,
                stepIndex,
                new QuestStepState(newState, newStatus)
            );
        }
    }
}