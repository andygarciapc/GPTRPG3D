using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic.Quests.Core
{
    public class QuestIcon : MonoBehaviour
    {
        [Header("Icons")]
        [SerializeField] private GameObject requirementsNotMetToStartIcon;
        [SerializeField] private GameObject canStartIcon;
        [SerializeField] private GameObject requirementsNotMetToFinishIcon;
        [SerializeField] private GameObject canFinishIcon;

        public void SetState(BasicQuestState newState, bool startPoint, bool finishPoint)
        {
            // set all to inactive
            requirementsNotMetToStartIcon.SetActive(false);
            canStartIcon.SetActive(false);
            requirementsNotMetToFinishIcon.SetActive(false);
            canFinishIcon.SetActive(false);

            // set the appropriate one to active based on the new state
            switch (newState)
            {
                case BasicQuestState.REQUIREMENTS_NOT_MET:
                    if (startPoint) { requirementsNotMetToStartIcon.SetActive(true); }
                    break;
                case BasicQuestState.CAN_START:
                    if (startPoint) { canStartIcon.SetActive(true); }
                    break;
                case BasicQuestState.IN_PROGRESS:
                    if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); }
                    break;
                case BasicQuestState.CAN_FINISH:
                    if (finishPoint) { canFinishIcon.SetActive(true); }
                    break;
                case BasicQuestState.FINISHED:
                    break;
                default:
                    Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                    break;
            }
        }
    }
}