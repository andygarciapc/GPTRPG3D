using UnityEngine;
namespace Basic.Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }

        public InputEvent inputEvents;
        public QuestEvents questEvents;
        public BasicCombatEvent combatEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            instance = this;

            // initialize all events
            combatEvents = new BasicCombatEvent();
            inputEvents = new InputEvent();
            questEvents = new QuestEvents();
        }
    }
}