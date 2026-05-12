using Confluent.Kafka;
using MessagePack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CarService.Models.Configurations;

namespace CarService.DL.Kafka
{
    public class KafkaProducer<TValue> : IKafkaProducer<TValue>, IDisposable
    {
        private readonly IProducer<string, byte[]> _producer;
        private readonly string _topic;
        private readonly ILogger<KafkaProducer<TValue>> _logger;

        public KafkaProducer(IOptions<KafkaConfiguration> config, ILogger<KafkaProducer<TValue>> logger)
        {
            _logger = logger;
            var kafkaConfig = config.Value;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaConfig.BootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = kafkaConfig.SaslUsername,
                SaslPassword = kafkaConfig.SaslPassword,
                EnableSslCertificateVerification = false
            };

            _producer = new ProducerBuilder<string, byte[]>(producerConfig).Build();
            _topic = kafkaConfig.Topic;
        }

        public async Task ProduceAsync(string key, TValue value)
        {
            var serialized = MessagePackSerializer.Serialize(value);

            var deliveryResult = await _producer.ProduceAsync(
                _topic,
                new Message<string, byte[]> { Key = key, Value = serialized });

            _logger.LogInformation(
                "Produced message to topic {Topic}, partition {Partition}, offset {Offset}",
                deliveryResult.Topic, deliveryResult.Partition, deliveryResult.Offset);
        }

        public void Dispose() => _producer?.Dispose();
    }
}
