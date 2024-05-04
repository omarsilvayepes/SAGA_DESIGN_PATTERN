namespace RabbitMq_Messages
{
    public class IOrderMessage
    {
        public Guid OrderId { get; set; }
        public string PaymentCardNumber { get; set; }
        public string ProductName { get; set; }
    }
}
