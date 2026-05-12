using MessagePack;

namespace CarService.Models.Messages
{
    [MessagePackObject]
    public class SellCarMessage
    {
        [Key(0)]
        public string CarModel { get; set; } = string.Empty;

        [Key(1)]
        public string CustomerName { get; set; } = string.Empty;

        [Key(2)]
        public decimal Price { get; set; }

        public SellCarMessage() { }

        public SellCarMessage(string carModel, string customerName, decimal price)
        {
            CarModel = carModel;
            CustomerName = customerName;
            Price = price;
        }
    }
}
