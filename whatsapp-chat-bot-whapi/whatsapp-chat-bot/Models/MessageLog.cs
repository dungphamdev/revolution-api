namespace whatsapp_chat_bot.Models
{
    public class MessageLog
    {
        public int Timestamp { get; set; }

        public string From { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
