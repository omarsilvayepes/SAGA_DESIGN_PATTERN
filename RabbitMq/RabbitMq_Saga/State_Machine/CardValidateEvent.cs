using RabbitMq_Messages;

namespace RabbitMq_Saga.State_Machine
{
    public class CardValidateEvent : ICardValidatorEvent
    {
        private readonly OrderStateData orderSagaState;
        public CardValidateEvent(OrderStateData orderStateData)
        {
            this.orderSagaState = orderStateData;
        }

        public Guid OrderId => orderSagaState.OrderId;
        public string PaymentCardNumber => orderSagaState.PaymentCardNumber;
        public string ProductName => orderSagaState.ProductName;
    }
}
