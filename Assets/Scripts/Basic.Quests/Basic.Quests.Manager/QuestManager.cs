using System.Collections.Generic;
using UnityEngine;
using System;
using Basic.Quests.Variable;
using Basic.Events;
using Basic.Quests.Core;

namespace Basic.Quests.Manager
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private bool loadQuestState = true;

        private Dictionary<string, BasicQuest> questMap;

        // quest start requirements
        private int currentPlayerLevel = 1;

        private void Awake()
        {
            questMap = CreateQuestMap();
        }

        private void OnEnable()
        {
            GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
            GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
            GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

            GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

            //GaneEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
            GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
            GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

            GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

            //GaneEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
        }

        private void Start()
        {

            foreach (BasicQuest quest in questMap.Values)
            {
                // initialize any loaded quest steps
                if (quest.state == BasicQuestState.IN_PROGRESS)
                {
                    quest.InstantiateCurrentQuestStep(this.transform);
                }
                // broadcast the initial state of all quests on startup
                GameEventsManager.instance.questEvents.QuestStateChange(quest);
            }
        }

        private void ChangeQuestState(string id, BasicQuestState state)
        {
            BasicQuest quest = GetQuestById(id);
            quest.state = state;
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }

        private void PlayerLevelChange(int level)
        {
            currentPlayerLevel = level;
        }

        private bool CheckRequirementsMet(BasicQuest quest)
        {
            // start true and prove to be false
            bool meetsRequirements = true;

            // check player level requirements
            if (currentPlayerLevel < quest.info.levelRequirement)
            {
                meetsRequirements = false;
            }

            // check quest prerequisites for completion
            foreach (BasicQuestInfo prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.id).state != BasicQuestState.FINISHED)
                {
                    meetsRequirements = false;
                }
            }

            return meetsRequirements;
        }

        private void Update()
        {
            // loop through ALL quests
            foreach (BasicQuest quest in questMap.Values)
            {
                // if we're now meeting the requirements, switch over to the CAN_START state
                if (quest.state == BasicQuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.info.id, BasicQuestState.CAN_START);
                }
            }
        }

        private void StartQuest(string id)
        {
            BasicQuest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, BasicQuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            BasicQuest quest = GetQuestById(id);

            // move on to the next step
            quest.MoveToNextStep();

            // if there are more steps, instantiate the next one
            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // if there are no more steps, then we've finished all of them for this quest
            else
            {
                ChangeQuestState(quest.info.id, BasicQuestState.CAN_FINISH);
            }
        }

        private void FinishQuest(string id)
        {
            BasicQuest quest = GetQuestById(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.info.id, BasicQuestState.FINISHED);
        }

        private void ClaimRewards(BasicQuest quest)
        {
            //ADD REWARD SYSTEM
            //GaneEventsManager.instance.goldEvents.GoldGained(quest.info.goldReward);
            //GaneEventsManager.instance.playerEvents.ExperienceGained(quest.info.experienceReward);
        }

        private void QuestStepStateChange(string id, int stepIndex, BasicQuestStepState questStepState)
        {
            BasicQuest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }

        private Dictionary<string, BasicQuest> CreateQuestMap()
        {
            // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
            BasicQuestInfo[] allQuests = Resources.LoadAll<BasicQuestInfo>("Quests");
            // Create the quest map
            Dictionary<string, BasicQuest> idToQuestMap = new Dictionary<string, BasicQuest>();
            foreach (BasicQuestInfo questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
            }
            return idToQuestMap;
        }

        private BasicQuest GetQuestById(string id)
        {
            BasicQuest quest = questMap[id];
            if (quest == null)
            {
                Debug.LogError("ID not found in the Quest Map: " + id);
            }
            return quest;
        }

        private void OnApplicationQuit()
        {
            foreach (BasicQuest quest in questMap.Values)
            {
                SaveQuest(quest);
            }
        }

        private void SaveQuest(BasicQuest quest)
        {
            try
            {
                BasicQuestData questData = quest.GetQuestData();
                // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
                string serializedData = JsonUtility.ToJson(questData);
                // saving to PlayerPrefs is just a quick example for this tutorial video,
                // you probably don't want to save this info there long-term.
                // instead, use an actual Save & Load system and write to a file, the cloud, etc..
                PlayerPrefs.SetString(quest.info.id, serializedData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
            }
        }

        private BasicQuest LoadQuest(BasicQuestInfo questInfo)
        {
            BasicQuest quest = null;
            try
            {
                // load quest from saved data
                if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
                {
                    string serializedData = PlayerPrefs.GetString(questInfo.id);
                    BasicQuestData questData = JsonUtility.FromJson<BasicQuestData>(serializedData);
                    quest = new BasicQuest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }
                // otherwise, initialize a new quest
                else
                {
                    quest = new BasicQuest(questInfo);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
            }
            return quest;
        }
    }
}