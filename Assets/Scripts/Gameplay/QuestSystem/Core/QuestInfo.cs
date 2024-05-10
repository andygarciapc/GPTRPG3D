using UnityEngine;
namespace Basic.Quests.Core
{
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "ScriptableObject/QuestInfo", order = 1)]
    public class QuestInfo : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }

        [Header("General")]
        public string displayName;
        public string questInfo;

        [Header("Requirements")]
        public int levelRequirement;
        public QuestInfo[] questPrerequisites;

        [Header("Steps")]
        public GameObject[] questStepPrefabs;

        [Header("Rewards")]
        public int goldReward;
        public int experienceReward;

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
            #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}
