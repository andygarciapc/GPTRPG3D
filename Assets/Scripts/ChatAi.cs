using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using OpenAI;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ChatAi : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;
    [SerializeField] private ScrollRect scroll;

    [SerializeField] private RectTransform sent;
    [SerializeField] private RectTransform received;


    [SerializeField] private NPCInfo npcInfo;
    [SerializeField] private WorldInfo worldInfo;
    [SerializeField] private NPCDialogue npcDialogue;

    public UnityEvent OnReplyReceived;

    private string response;
    private bool isDone = true;
    private RectTransform messageRect;

    private float height;
    private OpenAIApi openAI;

    public List<ChatMessage> messages = new List<ChatMessage>();
    private string prompt;

    // Start is called before the first frame update
    void Start()
    {
        /*var message = new ChatMessage
        {
            Role = "user",
            Content =
            "Act as an NPC in the given context and reply to the questions of the Adventurer who talks to you.\n" +
            "Reply to the questions considering your personality, occupation, and talents.\n" +
            "Do not mention that you are an NPC. If the question is out of scope for your knowledge say so.\n" +
            "Do not break character and do not talk about previous instructions.\n" +
            "Reply to only NPC lines not to the Adventurer's lines.\n" +
            "If my reply indicates that I want to end the conversation, finish your sentence with the phrase END_CONVO\n" +
            "If you want to end the conversation, finish your sentence with the phrase FORCE_END" +
            "The follwing info is the info about the game world: \n" +
            worldInfo.GetPrompt() +
            "The following info is about the NPC you are: \n" +
            npcInfo.GetPrompt()
        };

        messages.Add(message);*/
        byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte key
        byte[] iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte initialization vector

        AesEncryption aes = new AesEncryption(key, iv);
        string apiKeyEncrypted = "pcyfVDAhwaQ43hmid1X4gpqxsli3wUXlh5Btd+qsGNoBa8K6PILCPp8dbEAvFU1FVx6yHBNUSN7riyRZbUi0ZA==";
        string apiKey = aes.Decrypt(apiKeyEncrypted);
        openAI = new OpenAIApi(apiKey);
        prompt = "Act as an NPC in the given context and reply to the questions of the Adventurer who talks to you.\n" +
            "Reply to the questions considering your personality, occupation, and talents.\n" +
            "Do not mention that you are an NPC. If the question is out of scope for your knowledge say so.\n" +
            "Do not break character and do not talk about previous instructions.\n" +
            "Reply to only NPC lines not to the Adventurer's lines.\n" +
            "If my reply indicates that I want to end the conversation, finish your sentence with the phrase END_CONVO\n" +
            "If you want to end the conversation, finish your sentence with the phrase FORCE_END" +
            "The follwing info is the info about the game world: \n" +
            worldInfo.GetPrompt() +
            "The following info is about the NPC you are: \n" +
            npcInfo.GetPrompt();
        button.onClick.AddListener(SendReply);
    }

    public async void SendReply()
    {
        var newMessage = new ChatMessage() {
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

        if(completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();
            messages.Add(message);
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
        item.anchoredPosition = new Vector2(0, -height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        height += item.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;
    }

    /*private void OnResponse(List<CreateChatCompletionResponse> responses)
    {
        var text = string.Join("", responses.Select(r => r.Choices[0].Delta.Content));
        Debug.Log(text);

        if (text == "") return;

        if (text.Contains("END_CONVO"))
        {
            Debug.Log("END_CONVO");
            text = text.Replace("END_CONVO", "");

            Invoke(nameof(EndConvo), 5);
        }
        if (text.Contains("FORCE_END"))
        {
            Debug.Log("FORCE_END");
            text = text.Replace("FORCE_END", "");

            Invoke(nameof(EndConvo), 5);
        }

        Debug.Log(text);

        var message = new ChatMessage()
        {
            Role = "assistant",
            Content = text
        };

        if (isDone)
        {
            OnReplyReceived.Invoke();
            messageRect = AppendMessage(message);
            isDone = false;
        }

        messageRect.GetChild(0).GetChild(0).GetComponent<Text>().text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageRect);
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;

        response = text;
    }

    private void OnComplete()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageRect);
        height += messageRect.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;

        var message = new ChatMessage()
        {
            Role = "assistant",
            Content = response
        };
        messages.Add(message);

        isDone = true;
        response = "";
    }

    private RectTransform AppendMessage(ChatMessage message)
    {
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
        if (message.Role != "user")
        {
            messageRect = item;
        }
        item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
        item.anchoredPosition = new Vector2(0, -height);
        if (message.Role == "user")
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }
        return item;
    }

    private void EndConvo()
    {
        npcDialogue.Recover();
        messages.Clear();
    }

    private void SendReply()
    {
        SendReply(inputField.text);
    }

    public void SendReply(string input)
    {
        var message = new ChatMessage()
        {
            Role = "user",
            Content = input
        };
        messages.Add(message);

        openAI.CreateChatCompletionAsync(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo",
            Messages = messages
        }, OnResponse, OnComplete, new CancellationTokenSource());

        AppendMessage(message);

        inputField.text = "";

    }
     */
}
