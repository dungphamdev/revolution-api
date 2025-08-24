using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using whatsapp_chat_bot.Helpers;
using whatsapp_chat_bot.Models.Whapi.Messages;
using whatsapp_chat_bot.Models.Whapi.Webhook;

namespace whatsapp_chat_bot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WhapiWhatsAppMessageController : ControllerBase
    {
        private readonly ILogger<WhapiWhatsAppMessageController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public WhapiWhatsAppMessageController(
            ILogger<WhapiWhatsAppMessageController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpPost]
        [Route("messages")]
        public async Task<IActionResult> OnMessageReceive([FromBody] PostWebhookMessageRequest request)
        {
            bool isEnable = _config.GetValue<bool>("WhapiSettings:Enable");

            if (!isEnable) return Ok("Ignored");

            foreach (var msg in request.Messages)
            {
                if (msg.From_Me) continue;
                string from = msg.From;
                string body = msg.Text?.Body ?? string.Empty;

                _logger.LogInformation("[Whapi] Message from: {From}, Content: {Body}", from, body);

                if (!string.IsNullOrWhiteSpace(body))
                {
                    var responseMsg = MessageHelper.HandleMessageReceive(body, "Whapi");

                    if (!string.IsNullOrWhiteSpace(responseMsg))
                    {
                        var responseObj = new PostMessageTextRequest()
                        {
                            To = from,
                            Body = responseMsg
                        };

                        await SendTextMessage(responseObj);
                    }
                }
            }

            return Ok("Success");
        }

        [HttpPost]
        [Route("SendTextMessage")]
        public async Task<IActionResult> SendTextMessage(PostMessageTextRequest req)
        {
            bool isEnable = _config.GetValue<bool>("WhapiSettings:Enable");

            if (!isEnable) return Ok("Ignored");

            var httpClient = _httpClientFactory.CreateClient();

            var url = $"{_config.GetValue<string>("WhapiSettings:BaseApiAddress")}/messages/text";
            var jsonBody = JsonConvert.SerializeObject(req);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            // Add headers directly to request
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("authorization", $"Bearer {_config["WhapiSettings:InstanceToken"]}");

            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("WHAPI response: {Response}", responseContent);

            return Ok(responseContent);
        }
    }
}
