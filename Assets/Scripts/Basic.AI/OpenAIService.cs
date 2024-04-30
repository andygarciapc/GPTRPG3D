using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using Basic.Security;
using System.Text;
using System.Threading.Tasks;

namespace Basic.AI
{
    /// <summary>
    /// Handles communication with OpenAI API
    /// </summary>
    public class OpenAIService
    {
        private OpenAIApi openAI;
        private string apiKey;
        public OpenAIService()
        {
            InitializeOpenAI();
        }
        private void InitializeOpenAI()
        {
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            AesEncryption aes = new AesEncryption(key, iv);
            string apiKeyEncrypted = "pcyfVDAhwaQ43hmid1X4gpqxsli3wUXlh5Btd+qsGNoBa8K6PILCPp8dbEAvFU1FVx6yHBNUSN7riyRZbUi0ZA==";
            apiKey = aes.Decrypt(apiKeyEncrypted);

            openAI = new OpenAIApi(apiKey);
        }

        public async Task<ChatMessage> SendChatMessage(List<ChatMessage> messages)
        {
            var completionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0) return completionResponse.Choices[0].Message;
            return new ChatMessage()
            {
                Role = "null",
                Content = "null"
            };
        }

        public async Task<string> SendChatMessage(string prompt)
        {
            var completionResponse = await openAI.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<ChatMessage> { new ChatMessage { Content = prompt } }
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
                //response = completionResponse.Choices[0].Message.Content;
                return completionResponse.Choices[0].Message.Content;

            Debug.LogWarning("No text generated from prompt");
            return null;
        }
    }
}
