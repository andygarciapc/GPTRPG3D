using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using OpenAI;
using System.Text;
using BasicSecurity;


namespace BasicAI
{

    public class ChatAi : MonoBehaviour
    {
        //private serialized
        public InputField inputField;
        public Button button;
        public ScrollRect scroll;
        public RectTransform sent;
        public RectTransform received;
        public NPCInfo npcInfo;
        public WorldInfo worldInfo;
        public NPCDialogue npcDialogue;

        //private
        private float height;
        private OpenAIApi openAI;
        private string prompt;

        //public
        public UnityEvent OnReplyReceived;
        public List<ChatMessage> messages = new List<ChatMessage>();

        // Start is called before the first frame update
        void Start()
        {
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte key
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte initialization vector

            AesEncryption aes = new AesEncryption(key, iv);
            string apiKeyEncrypted = "pcyfVDAhwaQ43hmid1X4gpqxsli3wUXlh5Btd+qsGNoBa8K6PILCPp8dbEAvFU1FVx6yHBNUSN7riyRZbUi0ZA==";
            string apiKey = aes.Decrypt(apiKeyEncrypted);
            openAI = new OpenAIApi(apiKey);
            prompt = "Act as an NPC in the given context and reply to the questions of the Adventurer who talks to you.\n" +
                "Reply to the questions considering your personality, occupation, and talents.\n" +
                "Do not mention that you are an NPC. If the question is out of scope for your knowledge say so.\n" +
                //"Reply to only NPC lines not to the Adventurer's lines.\n" +
                "If I want to end the conversation, finish your sentence with the phrase END_CONVO\n" +
                "If you want to end the conversation, finish your sentence with the phrase FORCE_END" +
                "The follwing info is the info about the game world: \n" +
                worldInfo.GetPrompt() +
                "The following info is about the NPC you are: \n" +
                npcInfo.GetPrompt() +
                "Finally, do not break character and do not talk about the instructions before this.\n";
            button.onClick.AddListener(SendReply);
        }

        public async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text;

            messages.Add(newMessage);

            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;

            //Complete the instruction

            Debug.Log(newMessage.Content);
            var completionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                messages.Add(message);
                //CHECK FOR END_CONVO
                if (message.Content.Contains("END_CONVO"))
                {
                    message.Content = message.Content.Replace("END_CONVO", "");
                    EndConvo();
                }
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text generated from prompt");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        private void AppendMessage(ChatMessage message)
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

        public void EndConvo()
        {
            npcDialogue.QuitButton();
        }
    }
}
