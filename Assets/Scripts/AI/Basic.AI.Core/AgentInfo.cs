using UnityEngine;
using System;
using System.Collections.Generic;
namespace Basic.AI.Core
{
    public enum Occupation
    {
        MAGICIAN,
        KNIGHT,
        SAMURAI,
        DRUG_DEALER,
        NEMESIS,
        ZOMBIE
    }
    public enum Talent
    {
        MAGIC,
        STRONG,
        HUGE
    }
    public enum Quirk
    {
        CYNICAL,
        SOCIAL,
        POLITICAL,
        OPPORTUNIST,
        ARTISTIC
    }
    public enum Alignment
    {
        LAWFUL_GOOD,
        LAWFUL_NEUTRAL,
        LAWFUL_EVIL,
        NEUTRAL_GOOD,
        NEUTRAL_NEUTRAL,
        NEUTRAL_EVIL,
        CHAOTIC_GOOD,
        CHAOTIC_NEUTRAL,
        CHAOTIC_EVIL
    }
    public class BasicAgentInfo
    {
        public string agentName;
        public Occupation occupation;
        public Alignment alignment;
        public List<Talent> talents;
        public List<Quirk> quirks;
        public string about;
        public string Talents
        {
            get
            {
                return EnumListToString(talents);
            }
        }
        public string Quirks
        {
            get
            {
                return EnumListToString(quirks);
            }
        }
        public BasicAgentInfo()
        {
            talents = new List<Talent>();
            quirks = new List<Quirk>();
            agentName = "";
            about = "";
        }
        private string EnumListToString<T>(List<T> enumList) where T : Enum
        {
            string toReturn = "";
            foreach (T enumValue in enumList)
            {
                toReturn += enumValue + ", ";
            }
            toReturn = toReturn.Remove(toReturn.Length - 2, 2);
            toReturn = toReturn.Trim();
            return toReturn + "\n";
        }
        public string GetPrompt()
        {
            string prompt = $"NPC Name: {agentName}\n" +
                $"NPC Occupation: {occupation}\n" +
                $"NPC Talents: {Talents}" +
                $"NPC Quirks: {Quirks}" +
                $"NPC Alignment: {alignment}\n" +
                $"About NPC: {about}\n";
            //Debug.Log(prompt);
            return prompt;
        }
    }
    public class AgentInfo : MonoBehaviour
    {
        public string agentName;
        public Occupation occupation;
        public Alignment alignment;
        public List<Talent> talents;
        public List<Quirk> quirks;
        public string about;
        public BasicAgentInfo info;
        public void Start()
        {
            info = new BasicAgentInfo();
            info.agentName = agentName;
            info.occupation = occupation;
            info.alignment = alignment;
            info.talents = talents;
            info.quirks = quirks;
            info.about = about;
        }
    }
}
