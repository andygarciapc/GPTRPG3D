using UnityEngine;
using System.Collections.Generic;
using Basic.AI.Core;
namespace Basic.AI.Agents
{
    [CreateAssetMenu(fileName = "DevelopersAgentInfo", menuName = "ScriptableObject/DevelopersAgentInfo", order = 2)]
    public class DevelopersAgentInfo : ScriptableObject
    {
        public NPCInfo npcPrefab;
        public string id;

        private void OnValidate()
        {
            #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}
