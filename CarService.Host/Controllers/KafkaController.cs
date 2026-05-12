using CarService.DL.Kafka;
using CarService.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaProducer<ChatMessage> _producer;
        private readonly ILogger<KafkaController> _logger;

        public KafkaController(IKafkaProducer<ChatMessage> producer, ILogger<KafkaController> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromQuery] string sender, [FromQuery] string text)
        {
            var message = new ChatMessage(sender, text);
            await _producer.ProduceAsync(message.Id, message);

            _logger.LogInformation("Sent Kafka message from {Sender}: {Text}", sender, text);
            return Ok(new { message.Id, message.Sender, message.Text, message.Timestamp });
        }
    }
}
