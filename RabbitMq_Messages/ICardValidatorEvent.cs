namespace RabbitMq_Messages
{
    public interface ICardValidatorEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
