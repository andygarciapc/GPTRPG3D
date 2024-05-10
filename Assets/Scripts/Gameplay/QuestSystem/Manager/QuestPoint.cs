using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basic.Quests.Core;
using Basic.Quests.Variable;
using Basic.Events;

namespace Basic.Quests.Manager
{
    [RequireComponent(typeof(SphereCollider))]
    public class QuestPoint : MonoBehaviour
    {
        [Header("Quest")]
        [SerializeField] private QuestInfo questInfoForPoint;

        [Header("Config")]
        [SerializeField] private bool startPoint = true;
        [SerializeField] private bool finishPoint = true;

        private bool playerIsNear = false;
        private string questId;
        private QuestState currentQuestState;

        private QuestIcon questIcon;

        private void Awake()
        {
            questId = questInfoForPoint.id;
            questIcon = GetComponentInChildren<QuestIcon>();
        }

        private void OnEnable()
        {
            GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
            GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
            GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
        }

        private void SubmitPressed()
        {
            if (!playerIsNear) return;
            //Debug.Log("QuestPoint:SubmitPressed");
            // start or finish a quest
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                Debug.Log("Quest Started");
                GameEventsManager.instance.questEvents.StartQuest(questId);
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            {
                Debug.Log("Quest Finished");
                GameEventsManager.instance.questEvents.FinishQuest(questId);
            }
        }

        private void QuestStateChange(Quest quest)
        {
            // only update the quest state if this point has the corresponding quest
            if (quest.info.id.Equals(questId))
            {
                currentQuestState = quest.state;
                questIcon.SetState(currentQuestState, startPoint, finishPoint);
            }
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                playerIsNear = true;
            }
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                playerIsNear = false;
            }
        }
    }
}