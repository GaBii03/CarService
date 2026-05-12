namespace CarService.DL.Kafka
{
    public interface IKafkaProducer<TValue>
    {
        Task ProduceAsync(string key, TValue value);
    }
}
