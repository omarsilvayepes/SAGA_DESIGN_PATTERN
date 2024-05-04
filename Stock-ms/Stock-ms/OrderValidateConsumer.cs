using MassTransit;
using RabbitMq_Messages;

namespace Stock_ms
{
    public class OrderValidateConsumer: IConsumer<ICardValidatorEvent>
    {
        public async Task Consume(ConsumeContext<ICardValidatorEvent> context)
        {
            var data = context.Message;
        }
    }
}
