using System.Text;
using whatsapp_chat_bot.Models.Whapi.Webhook;

namespace whatsapp_chat_bot.Helpers
{
    public class MessageHelper
    {
        public static string HandleMessageReceive(string message, string provider = "")
        {
            var startMessage = new string[] { "hello", "start", "hi" };

            StringBuilder result = new();
            if (startMessage.Any(x => message.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
            {
                result.AppendLine("Hello! How can I help you today?");
            }
            else
            {
                result.AppendLine($"You asked: \"{message}\"");
                result.AppendLine("Here’s a possible solution:");
                result.AppendLine("...");
            }

            if (!string.IsNullOrWhiteSpace(provider))
            {
                result.AppendLine($"\n`Send from {provider}`");
            }

            return result.ToString();
        }
    }
}
