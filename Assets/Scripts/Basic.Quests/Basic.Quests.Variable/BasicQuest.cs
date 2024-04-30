using UnityEngine;
using Basic.Quests.Core;
namespace Basic.Quests.Variable
{

    public class BasicQuest : MonoBehaviour
    {
        // static info
        public BasicQuestInfo info;

        // state info
        public BasicQuestState state;
        private int currentQuestStepIndex;
        private BasicQuestStepState[] questStepStates;

        public BasicQuest(BasicQuestInfo questInfo)
        {
            this.info = questInfo;
            this.state = BasicQuestState.REQUIREMENTS_NOT_MET;
            this.currentQuestStepIndex = 0;
            this.questStepStates = new BasicQuestStepState[info.questStepPrefabs.Length];
            for (int i = 0; i < questStepStates.Length; i++)
            {
                questStepStates[i] = new BasicQuestStepState();
            }
        }

        public BasicQuest(BasicQuestInfo questInfo, BasicQuestState questState, int currentQuestStepIndex, BasicQuestStepState[] questStepStates)
        {
            this.info = questInfo;
            this.state = questState;
            this.currentQuestStepIndex = currentQuestStepIndex;
            this.questStepStates = questStepStates;

            // if the quest step states and prefabs are different lengths,
            // something has changed during development and the saved data is out of sync.
            if (this.questStepStates.Length != this.info.questStepPrefabs.Length)
            {
                Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                    + "of different lengths. This indicates something changed "
                    + "with the QuestInfo and the saved data is now out of sync. "
                    + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
            }
        }

        public void MoveToNextStep()
        {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (currentQuestStepIndex < info.questStepPrefabs.Length);
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                BasicQuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform)
                    .GetComponent<BasicQuestStep>();
                questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
            }
            else
            {
                Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                    + "there's no current step: QuestId=" + info.id + ", stepIndex=" + currentQuestStepIndex);
            }
            return questStepPrefab;
        }

        public void StoreQuestStepState(BasicQuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < questStepStates.Length)
            {
                questStepStates[stepIndex].state = questStepState.state;
                questStepStates[stepIndex].status = questStepState.status;
            }
            else
            {
                Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: "
                    + "Quest Id = " + info.id + ", Step Index = " + stepIndex);
            }
        }

        public BasicQuestData GetQuestData()
        {
            return new BasicQuestData(state, currentQuestStepIndex, questStepStates);
        }

        public string GetFullStatusText()
        {
            string fullStatus = "";

            if (state == BasicQuestState.REQUIREMENTS_NOT_MET)
            {
                fullStatus = "Requirements are not yet met to start this quest.";
            }
            else if (state == BasicQuestState.CAN_START)
            {
                fullStatus = "This quest can be started!";
            }
            else
            {
                // display all previous quests with strikethroughs
                for (int i = 0; i < currentQuestStepIndex; i++)
                {
                    fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
                }
                // display the current step, if it exists
                if (CurrentStepExists())
                {
                    fullStatus += questStepStates[currentQuestStepIndex].status;
                }
                // when the quest is completed or turned in
                if (state == BasicQuestState.CAN_FINISH)
                {
                    fullStatus += "The quest is ready to be turned in.";
                }
                else if (state == BasicQuestState.FINISHED)
                {
                    fullStatus += "The quest has been completed!";
                }
            }

            return fullStatus;
        }
    }
}