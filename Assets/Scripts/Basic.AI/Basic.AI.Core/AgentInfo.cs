using UnityEngine;
namespace Basic.AI.Core
{
    public enum Occupation
    {
        Magician,
        Knight,
        Samurai,
        Drug_Dealer,
        Nemesis
    }
    public enum Talent
    {
        Magic,
        Strong,
        Huge
    }
    public enum Personality
    {
        Cynical,
        Social,
        Political,
        Opportunist,
        Artistic
    }
    public class AgentInfo : MonoBehaviour
    {
        public string name = "";
        public Occupation occupation;
        public Talent talents;
        public Personality npcPersonality;
    }
}
