using Newtonsoft.Json;

namespace whatsapp_chat_bot.Models.EvolutionApi.Webhook
{
    public class WebhookMessageUpsertRequest
    {
        [JsonProperty("event")]
        public string Event { get; set; } = string.Empty;

        [JsonProperty("instance")]
        public string Instance { get; set; } = string.Empty;

        [JsonProperty("data")]
        public Data Data { get; set; } = new();

        [JsonProperty("destination")]
        public string Destination { get; set; } = string.Empty;

        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; } = string.Empty;

        [JsonProperty("server_url")]
        public string ServerUrl { get; set; } = string.Empty;

        [JsonProperty("apikey")]
        public string Apikey { get; set; } = string.Empty;
    }

    public class Data
    {
        [JsonProperty("key")]
        public Key Key { get; set; } = new();

        [JsonProperty("pushName")]
        public string PushName { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("message")]
        public Message Message { get; set; } = new();

        [JsonProperty("messageType")]
        public string MessageType { get; set; } = string.Empty;

        [JsonProperty("messageTimestamp")]
        public int MessageTimestamp { get; set; }

        [JsonProperty("instanceId")]
        public string InstanceId { get; set; } = string.Empty;

        [JsonProperty("source")]
        public string Source { get; set; } = string.Empty;
    }

    public class DeviceListMetadata
    {
        [JsonProperty("senderKeyHash")]
        public string SenderKeyHash { get; set; } = string.Empty;

        [JsonProperty("senderTimestamp")]
        public string SenderTimestamp { get; set; } = string.Empty;

        [JsonProperty("senderAccountType")]
        public string SenderAccountType { get; set; } = string.Empty;

        [JsonProperty("receiverAccountType")]
        public string ReceiverAccountType { get; set; } = string.Empty;

        [JsonProperty("recipientKeyHash")]
        public string RecipientKeyHash { get; set; } = string.Empty;

        [JsonProperty("recipientTimestamp")]
        public string RecipientTimestamp { get; set; } = string.Empty;
    }

    public class Key
    {
        [JsonProperty("remoteJid")]
        public string RemoteJid { get; set; } = string.Empty;

        [JsonProperty("fromMe")]
        public bool FromMe { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("senderLid")]
        public string SenderLid { get; set; } = string.Empty;
    }

    public class Message
    {
        [JsonProperty("conversation")]
        public string Conversation { get; set; } = string.Empty;

        [JsonProperty("messageContextInfo")]
        public MessageContextInfo MessageContextInfo { get; set; } = new();
    }

    public class MessageContextInfo
    {
        [JsonProperty("deviceListMetadata")]
        public DeviceListMetadata DeviceListMetadata { get; set; } = new();

        [JsonProperty("deviceListMetadataVersion")]
        public int DeviceListMetadataVersion { get; set; }

        [JsonProperty("messageSecret")]
        public string MessageSecret { get; set; } = string.Empty;
    }
}
