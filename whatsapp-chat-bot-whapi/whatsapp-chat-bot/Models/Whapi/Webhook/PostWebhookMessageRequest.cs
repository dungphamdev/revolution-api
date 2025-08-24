using Newtonsoft.Json;
using whatsapp_chat_bot.Models.Whapi.Messages;

namespace whatsapp_chat_bot.Models.Whapi.Webhook
{
    public class PostWebhookMessageRequest
    {
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = [];

        [JsonProperty("channel_id")]
        public string Channel_Id { get; set; } = string.Empty;
    }

    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("from_me")]
        public bool From_Me { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("chat_id")]
        public string Chat_Id { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; } = string.Empty;

        [JsonProperty("text")]
        public Text Text { get; set; } = new();

        [JsonProperty("from")]
        public string From { get; set; } = string.Empty;

        [JsonProperty("from_name")]
        public string FromName { get; set; } = string.Empty;
    }

    public class Text
    {
        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;
    }
}
