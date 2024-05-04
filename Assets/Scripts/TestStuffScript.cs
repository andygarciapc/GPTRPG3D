using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basic.AI.Core;
using System.Threading.Tasks;
using OpenAI;
public class TestStuffScript : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log("TestStuffScript:Start()\n");
        AgentInfo agentInfo = await AITools.GenerateAgentInfo();
        AreaInfo areaInfo = await AITools.GenerateAreaInfo();
        ChatMessage starterMessage = await AITools.GenerateStarterMessage(agentInfo.info, areaInfo.info, "A quest about killing things");
    }
}
