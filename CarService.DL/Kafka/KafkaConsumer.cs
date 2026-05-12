using Confluent.Kafka;
using MessagePack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CarService.Models.Configurations;

namespace CarService.DL.Kafka
{
    public class KafkaConsumer<TValue> : BackgroundService
    {
        private readonly KafkaConfiguration _config;
        private readonly ILogger<KafkaConsumer<TValue>> _logger;

        protected virtual string GroupId => _config.GroupId;

        public KafkaConsumer(IOptions<KafkaConfiguration> config, ILogger<KafkaConsumer<TValue>> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => StartConsuming(stoppingToken), stoppingToken);
        }

        private void StartConsuming(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config.BootstrapServers,
                GroupId = GroupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = _config.SaslUsername,
                SaslPassword = _config.SaslPassword,
                EnableSslCertificateVerification = false
            };

            using var consumer = new ConsumerBuilder<string, byte[]>(consumerConfig).Build();
            consumer.Subscribe(_config.Topic);

            _logger.LogInformation("Kafka consumer started. Listening on topic: {Topic}", _config.Topic);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);
                    var message = MessagePackSerializer.Deserialize<TValue>(result.Message.Value);

                    _logger.LogInformation(
                        "Received Kafka message: key={Key}, type={Type}",
                        result.Message.Key, typeof(TValue).Name);

                    HandleMessage(message);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer is stopping.");
            }
            finally
            {
                consumer.Close();
            }
        }

        protected virtual void HandleMessage(TValue message)
        {
            _logger.LogInformation("Kafka message received: {Message}", message);
        }
    }
}
