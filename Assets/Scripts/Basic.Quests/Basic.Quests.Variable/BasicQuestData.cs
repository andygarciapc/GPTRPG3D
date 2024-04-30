using Basic.Quests.Core;
namespace Basic.Quests.Variable
{
    [System.Serializable]
    public class BasicQuestData
    {
        public BasicQuestState state;
        public int questStepIndex;
        public BasicQuestStepState[] questStepStates;

        public BasicQuestData(BasicQuestState state, int questStepIndex, BasicQuestStepState[] questStepStates)
        {
            this.state = state;
            this.questStepIndex = questStepIndex;
            this.questStepStates = questStepStates;
        }
    }
}