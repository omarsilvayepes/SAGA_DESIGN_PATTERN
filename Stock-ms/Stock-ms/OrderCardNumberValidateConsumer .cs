using MassTransit;
using RabbitMq_Messages;

namespace Stock_ms
{
    public class OrderCardNumberValidateConsumer : IConsumer<IOrderMessage>
    {
        public async Task Consume(ConsumeContext<IOrderMessage> context)
        {
            var data = context.Message;
            if (data.PaymentCardNumber != "test")
            {
                // invalid
            }
        }
    }
}
