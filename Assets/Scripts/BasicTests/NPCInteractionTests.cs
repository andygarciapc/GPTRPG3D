using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BasicAI;
using OpenAI;

public class NPCInteractionTests
{
    private GameObject npcGameObject;
    private NPCInfo npcInfo;
    [SetUp]
    public void SetUp()
    {
        npcGameObject = new GameObject();
        npcInfo = npcGameObject.AddComponent<NPCInfo>();

        // Setup NPCInfo properties as needed for tests
        npcInfo.name = "TestNPC";
        npcInfo.occupation = Occupation.Magician;
        npcInfo.talents = Talent.Magic;
        npcInfo.npcPersonality = Personality.Social;
    }

    [Test]
    public void NPCInfo_HasCorrectInitialValues()
    {
        Assert.AreEqual("TestNPC", npcInfo.name);
        Assert.AreEqual(Occupation.Magician, npcInfo.occupation);
        Assert.AreEqual(Talent.Magic, npcInfo.talents);
        Assert.AreEqual(Personality.Social, npcInfo.npcPersonality);
    }
}
