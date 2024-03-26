using UnityEngine;

namespace BasicAI
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
        [SerializeField] protected string name = "";
        [SerializeField] protected Occupation occupation;
        [SerializeField] protected Talent talents;
        [SerializeField] protected Personality npcPersonality;
    }

}
