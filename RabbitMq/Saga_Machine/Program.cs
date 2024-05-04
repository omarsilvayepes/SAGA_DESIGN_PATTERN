using MassTransit;
using MassTransit.Definition;
using MassTransit.Saga;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RabbitMq;
using RabbitMq_Saga.State_Machine;

namespace Saga_Machine
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var saga = new OrderStateMachine();
            var repo = new InMemorySagaRepository<OrderStateData>();

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
                   services.AddMassTransit(cfg =>
                   {
                       cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider, (cfg, host) =>
                       {
                           cfg.ReceiveEndpoint(BusConstants.SagaBusQueue, e =>
                           {
                               e.StateMachineSaga(saga, repo);
                           });
                       }));

                   });
                   services.AddMassTransitHostedService();
               });

            await builder.RunConsoleAsync();
        }
    }
}