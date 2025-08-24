using Newtonsoft.Json;

namespace whatsapp_chat_bot.Models.EvolutionApi.Messages
{
    public class PostSendTextMessageRequest
    {

        [JsonProperty("instance")]
        public string Instance { get; set; } = string.Empty;

        [JsonProperty("apikey")]
        public string Apikey { get; set; } = string.Empty;

        [JsonProperty("number")]
        public string Number { get; set; } = string.Empty;

        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
