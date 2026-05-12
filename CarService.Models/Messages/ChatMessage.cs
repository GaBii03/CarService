using MessagePack;

namespace CarService.Models.Messages
{
    [MessagePackObject]
    public class ChatMessage
    {
        [Key(0)]
        public string Id { get; set; } = string.Empty;

        [Key(1)]
        public string Sender { get; set; } = string.Empty;

        [Key(2)]
        public string Text { get; set; } = string.Empty;

        [Key(3)]
        public DateTime Timestamp { get; set; }

        public ChatMessage() { }

        public ChatMessage(string sender, string text)
        {
            Id = Guid.NewGuid().ToString();
            Sender = sender;
            Text = text;
            Timestamp = DateTime.Now;
        }
    }
}
