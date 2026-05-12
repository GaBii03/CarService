using CarService.Models.Configurations;
using CarService.Models.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarService.DL.Kafka
{
    public class SellCarKafkaConsumer : KafkaConsumer<SellCarMessage>
    {
        private readonly ILogger<SellCarKafkaConsumer> _logger;

        protected override string GroupId => "sell-car-group";

        public SellCarKafkaConsumer(
            IOptions<KafkaConfiguration> config,
            ILogger<SellCarKafkaConsumer> logger)
            : base(config, logger)
        {
            _logger = logger;
        }

        protected override void HandleMessage(SellCarMessage message)
        {
            _logger.LogInformation(
                "[KAFKA] Car sold! Car: {Car}, Customer: {Customer}, Price: {Price}",
                message.CarModel, message.CustomerName, message.Price);
        }
    }
}
