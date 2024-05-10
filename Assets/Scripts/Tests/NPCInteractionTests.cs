using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Basic.AI.Core;
using OpenAI;
namespace Basic.Tests
{
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
            npcInfo.agentName = "TestNPC";
            npcInfo.occupation = Occupation.MAGICIAN;
            //npcInfo.talents = Talent.MAGIC;
            //npcInfo.npcPersonality = Quirk.SOCIAL;
        }

        [Test]
        public void NPCInfo_HasCorrectInitialValues()
        {
            Assert.AreEqual("TestNPC", npcInfo.agentName);
            Assert.AreEqual(Occupation.MAGICIAN, npcInfo.occupation);
            //Assert.AreEqual(Talent.MAGIC, npcInfo.talents);
            //Assert.AreEqual(Quirk.SOCIAL, npcInfo.npcPersonality);
        }
    }
}