using UnityEngine;
namespace Basic.AI.Core
{
    public class NPCInfo : AgentInfo
    {
        public string GetPrompt()
        {
            return $"NPC Name: {name}\n" +
                $"NPC Occupation: {occupation}\n" +
                $"NPC Talents: {talents}\n" +
                $"NPC Personality: {npcPersonality}\n";
        }
    }
}