namespace Basic.Quests.Core
{
    [System.Serializable]
    public class BasicQuestStepState
    {
        public string state;
        public string status;

        public BasicQuestStepState(string state, string status)
        {
            this.state = state;
            this.status = status;
        }

        public BasicQuestStepState()
        {
            this.state = "";
            this.status = "";
        }
    }
}