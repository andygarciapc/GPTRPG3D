using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using OpenAI;
using System.Text;
using Basic.Security;
using Basic.AI.Core;
using Basic.AI;
using UnityEngine.SceneManagement;
using Basic.Events;

namespace Basic.AI.Game
{
    /// <summary>
    /// Chat AI Class
    /// </summary>
    public class ChatAi : MonoBehaviour
    {
        //private serialized
        public InputField inputField;
        public Button button;
        public ScrollRect scroll;
        public RectTransform sent;
        public RectTransform received;
        public NPCInfo npcInfo;
        public AreaInfo areaInfo;
        public NPCDialogue npcDialogue;
        public bool isDialogueScene;

        //private
        private float height;
        //private OpenAIApi openAI;
        private OpenAIService openAIService;

        //public
        public UnityEvent OnReplyReceived;
        public List<ChatMessage> messages = new List<ChatMessage>();

        private void Awake()
        {
            openAIService = new OpenAIService();
            isDialogueScene = SceneManager.GetActiveScene().name == "Dialogue";
            button.onClick.AddListener(SendReply);
        }

        private void OnEnable()
        {
            GameEventsManager.instance.inputEvents.onEscapePressed += EndConvo;
        }
        private void OnDisable()
        {
            GameEventsManager.instance.inputEvents.onEscapePressed -= EndConvo;
        }

        // Start is called before the first frame update
        async void Start() {  
            if (isDialogueScene)
            {
                ChatMessage starterMessage = await AITools.GenerateStarterMessage(npcInfo.info, areaInfo.info, "A quest about killing stuff");
                ChatMessage promptMessage = new ChatMessage()
                {
                    Role = "user",
                    Content = GetPrompt()
                };
                messages.Add(promptMessage);
                AppendMessage(starterMessage);
                messages.Add(starterMessage);
            }
        }
        private void Update()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        /// <summary>
        /// Used by SendReply()
        /// </summary>
        private void AddNewMessage()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            AppendMessage(newMessage);
            if (messages.Count == 0) newMessage.Content = GetPrompt() + "\n" + inputField.text;
            messages.Add(newMessage);
        }
        public string GetPrompt()
        {
            return "Act as an NPC in the given context and reply to the questions of the Adventurer who talks to you.\n" +
                "Reply to the questions considering your quirks, occupation, and talents.\n" +
                "Do not mention that you are an NPC. If the question is out of scope for your knowledge say so.\n" +
                //"Reply to only NPC lines not to the Adventurer's lines.\n" +
                "Once you want to end the conversation and send the player to the next scene, finish your sentence with the phrase END_CONVO\n" +
                "The follwing info is the info about the game world: \n" +
                areaInfo.info.GetPrompt() +
                "The following info is about the NPC you are: \n" +
                npcInfo.info.GetPrompt() +
                "Here is the players agentName: " + PlayerPrefs.GetString("Username") +
                "Finally, do not break character and do not talk about the instructions before this.\n";
        }
        /// <summary>
        /// Used by SendReply()
        /// </summary>
        private void EnableInput()
        {
            button.enabled = true;
            inputField.enabled = true;
        }
        /// <summary>
        /// Used by SendReply()
        /// </summary>
        private void DisableInput()
        {
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
        }
        /// <summary>
        /// Manages Chat UI
        /// </summary>
        public async void SendReply()
        {
            if(inputField.text.Length <= 1) { return; }
            AddNewMessage();
            DisableInput();
            ChatMessage response = await openAIService.SendChatMessage(messages);

            if (response.Content != "null")
            {
                messages.Add(response);
                //CHECK FOR END_CONVO
                if (response.Content.Contains("END_CONVO"))
                {
                    response.Content = response.Content.Replace("END_CONVO", "");
                    EndConvo();
                }
                AppendMessage(response);
            }
            else Debug.LogWarning("No text generated from prompt");
            EnableInput();
        }

        /// <summary>
        /// Append a ChatMessage to the Chatbox UI
        /// </summary>
        /// <param name="message"></param>
        public void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;

            float initialYPositionOffset = -10f;

            item.anchoredPosition = new Vector2(0, -height + initialYPositionOffset);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        /// <summary>
        /// End the conversation with NPC
        /// </summary>
        public async void EndConvo()
        {
            //npcDialogue.QuitButton();
            Debug.Log("EndConvo()");
            if(npcDialogue != null) { npcDialogue.QuitButton(); }
            else
            {
                if (!isDialogueScene) return;
                string scene = await AITools.ChooseNextScene(areaInfo.info, npcInfo.info, "A quest about killing some zombies.");
                if (scene == "GENERIC") {
                    Debug.Log("CHATGPT CHOOSES GENERIC SCENE");
                    SceneManager.LoadScene("Debug");
                }
                return;
            }
        }
    }
}
