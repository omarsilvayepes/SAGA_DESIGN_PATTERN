﻿namespace RabbitMq_Messages
{
    public interface IOrderStartEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
