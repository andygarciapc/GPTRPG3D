using System;
using UnityEngine;
namespace Basic.Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }

        public InputEvents inputEvents;
        public PlayerEvents playerEvents;
        public QuestEvents questEvents;
        public CombatEvents combatEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            instance = this;
            // initialize all events
            inputEvents = new InputEvents();
            playerEvents = new PlayerEvents();
            questEvents = new QuestEvents();
            combatEvents = new CombatEvents();
        }
    }
}