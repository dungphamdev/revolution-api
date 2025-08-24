using Newtonsoft.Json;

namespace whatsapp_chat_bot.Models.Whapi.Messages
{
    public class PostMessageTextRequest
    {
        [JsonProperty("to")]
        public string To { get; set; } = string.Empty;

        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;
    }
}
