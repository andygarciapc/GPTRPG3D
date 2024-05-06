using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basic.AI.Agents;
public class ResourceTools
{
    private Dictionary<string, DevelopersAgentInfo> CreateAgentMap()
    {
        // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
        DevelopersAgentInfo[] allInfos = Resources.LoadAll<DevelopersAgentInfo>("DeveloperAgents");
        // Create the quest map
        Dictionary<string, DevelopersAgentInfo> idToQuestMap = new Dictionary<string, DevelopersAgentInfo>();
        foreach (DevelopersAgentInfo info in allInfos)
        {
            if (idToQuestMap.ContainsKey(info.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + info);
            }
            idToQuestMap.Add(info.id, info);
        }
        return idToQuestMap;
    }
}
