using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text;
using whatsapp_chat_bot.Helpers;
using whatsapp_chat_bot.Models;
using whatsapp_chat_bot.Models.EvolutionApi.Messages;
using whatsapp_chat_bot.Models.EvolutionApi.Webhook;

namespace whatsapp_chat_bot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EvolutionApiWhatsAppMessageController : ControllerBase
    {
        private readonly ILogger<EvolutionApiWhatsAppMessageController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private static readonly ConcurrentQueue<MessageLog> MessageLogs = new();

        public EvolutionApiWhatsAppMessageController(
            ILogger<EvolutionApiWhatsAppMessageController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpPost]
        [Route("messages-upsert")]
        public async Task<IActionResult> OnMessageReceive([FromBody] WebhookMessageUpsertRequest request)
        {
            bool isEnable = _config.GetValue<bool>("EvolutionSettings:Enable");

            //if (!isEnable || request.Data.Key.FromMe) return Ok("Ignored");
            if (!isEnable) return Ok("Ignored");

            var messageText = request.Data.Message.Conversation;
            var from = request.Data.Key.FromMe ? request.Sender : request.Data.Key.RemoteJid;
            var timestamp = request.Data.MessageTimestamp;

            MessageLogs.Enqueue(new MessageLog
            {
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime,
                From = from,
                Content = request.Data.Message.Conversation
            });

            _logger.LogInformation("[EvolutionApi] Message from: {From}, Content: {Body}",
                from, messageText);

            //if (!string.IsNullOrWhiteSpace(messageText))
            //{
            //    var responseMsg = MessageHelper.HandleMessageReceive(messageText, "Evolution Api");

            //    if (!string.IsNullOrWhiteSpace(responseMsg))
            //    {
            //        var responseObj = new PostSendTextMessageRequest
            //        {
            //            Instance = request.Instance,
            //            Apikey = request.Apikey,
            //            Number = from,
            //            Text = responseMsg
            //        };

            //        await SendTextMessage(responseObj);
            //    }
            //}

            //_logger.LogInformation("[Raw JSON] {Json}", body);

            return Ok("Success");
        }

        [HttpPost]
        [Route("send-text-message")]
        public async Task<IActionResult> SendTextMessage(PostSendTextMessageRequest req)
        {
            bool isEnable = _config.GetValue<bool>("EvolutionSettings:Enable");

            if (!isEnable) return Ok("Ignored");

            var httpClient = _httpClientFactory.CreateClient();

            var url = $"{_config.GetValue<string>("EvolutionSettings:BaseApiAddress")}/message/sendText/{req.Instance}";
            var jsonBody = JsonConvert.SerializeObject(req);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            // Add headers directly to request
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("apikey", req.Apikey);

            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("Evolution API response: {Response}", responseContent);

            return Ok(responseContent);
        }
    }
}
