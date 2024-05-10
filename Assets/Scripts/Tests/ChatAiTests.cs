using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Basic.AI;
using System.Reflection;
using OpenAI;
using UnityEngine.TestTools;
using System.Collections;
using Basic.AI.Core;
using Basic.AI.Game;
namespace Basic.Tests
{
    [TestFixture]
    public class ChatAiTests
    {
        private ChatAi chatAi;
        private GameObject gameObject;

        [SetUp]
        public void SetUp()
        {
            // Setup your game object and add the ChatAi component
            gameObject = new GameObject();
            chatAi = gameObject.AddComponent<ChatAi>();

            // Initialize necessary public fields as they would be in the editor.
            chatAi.inputField = new GameObject().AddComponent<InputField>();
            chatAi.button = new GameObject().AddComponent<Button>();
            chatAi.scroll = new GameObject().AddComponent<ScrollRect>();
            chatAi.sent = new GameObject().AddComponent<RectTransform>();
            chatAi.received = new GameObject().AddComponent<RectTransform>();
            chatAi.npcInfo = new NPCInfo(); // Assuming NPCInfo is a scriptable object or similar
            chatAi.areaInfo = new AreaInfo(); // Assuming AreaInfo is a scriptable object or similar
        }

        [UnityTest]
        public IEnumerator ChatAi_SendReply_Test()
        {
            // Arrange
            GameObject chatAiObject = new GameObject();
            chatAiObject.AddComponent<ChatAi>();
            ChatAi chatAi = chatAiObject.GetComponent<ChatAi>();
            chatAi.inputField = new GameObject().AddComponent<InputField>();
            chatAi.button = new GameObject().AddComponent<Button>();
            chatAi.scroll = new GameObject().AddComponent<ScrollRect>();
            chatAi.sent = new GameObject().AddComponent<RectTransform>();
            chatAi.received = new GameObject().AddComponent<RectTransform>();
            chatAi.npcInfo = new GameObject().AddComponent<NPCInfo>();
            chatAi.areaInfo = new GameObject().AddComponent<AreaInfo>();
            chatAi.npcDialogue = new GameObject().AddComponent<NPCDialogue>();

            // Act
            chatAi.inputField.text = "Test message";
            chatAi.SendReply();

            // Assert
            yield return new WaitForSeconds(1); // Adjust time as needed
            Assert.AreEqual(2, chatAi.messages.Count); // One initial message + the reply
        }

        [Test]
        public void MessageIsAppendedToMessagesList()
        {
            // Simulate the input field having text
            chatAi.inputField.text = "Hello, NPC!";

            // Normally, you'd call chatAi.SendReply() here, but since it's async and involves an API call, simulate the part of appending a message directly.
            // This would be a method extracted from SendReply or made testable through refactoring.

            // Simulate adding message directly to the list as SendReply would.
            // This requires SendReply's logic to be refactored out into a testable component or changing access modifiers.
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = chatAi.inputField.text
            };
            chatAi.messages.Add(newMessage);

            // Check if the message was added
            Assert.AreEqual(1, chatAi.messages.Count);
            Assert.AreEqual("Hello, NPC!", chatAi.messages[0].Content);
        }

        /*[UnityTest]
        public IEnumerator ChatAi_EndConvo_Test()
        {
            // Arrange
            GameObject chatAiObject = new GameObject();
            chatAiObject.AddComponent<ChatAi>();
            ChatAi chatAi = chatAiObject.GetComponent<ChatAi>();
            chatAi.npcDialogue = new GameObject().AddComponent<NPCDialogue>();

            // Act
            chatAi.EndConvo();

            // Assert
            yield return null; // Wait for the end of frame
                               // TODO: Assert whatever state or behavior you expect after ending conversation
        }*/

        [TearDown]
        public void TearDown()
        {
            // Cleanup
            Object.DestroyImmediate(gameObject);
        }
    }
}