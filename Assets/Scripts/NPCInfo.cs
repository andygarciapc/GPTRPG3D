using UnityEngine;

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

public class NPCInfo : MonoBehaviour
{
    [SerializeField] private string npcName = "";
    [SerializeField] private Occupation npcOccupation;
    [SerializeField] private Talent npcTalents;
    [SerializeField] private Personality npcPersonality;

    public string GetPrompt()
    {
        return $"NPC Name: {npcName}\n" + 
            $"NPC Occupation: {npcOccupation}\n" +
            $"NPC Talents: {npcTalents}\n" +
            $"NPC Personality: {npcPersonality}\n";
    }
}
