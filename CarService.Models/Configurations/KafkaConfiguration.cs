namespace CarService.Models.Configurations
{
    public class KafkaConfiguration
    {
        public required string BootstrapServers { get; set; }
        public required string Topic { get; set; }
        public string GroupId { get; set; } = "car-service-group";
        public string? SaslUsername { get; set; }
        public string? SaslPassword { get; set; }
    }
}
