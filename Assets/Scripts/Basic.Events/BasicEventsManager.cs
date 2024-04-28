using UnityEngine;
namespace Basic.Events
{
    public class BasicEventsManager : MonoBehaviour
    {
        public static BasicEventsManager instance { get; private set; }

        public BasicInputEvent inputEvents;
        public BasicQuestEvent questEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            instance = this;

            // initialize all events
            inputEvents = new BasicInputEvent();
            questEvents = new BasicQuestEvent();
        }
    }
}