using System;
using System.Threading.Tasks;
using Contracts;
using Hub.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Hub.Consumer
{
    public class BroadcastMessageConsumerDefinition : ConsumerDefinition<BroadcastMessageConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<BroadcastMessageConsumer> consumerConfigurator)
        {
            if(endpointConfigurator is IRabbitMqReceiveEndpointConfigurator e)
            {
                e.AutoDelete = true;
                e.QueueExpiration = TimeSpan.FromSeconds(300);
            }
        }
    }

    public class BroadcastMessageConsumer : IConsumer<BroadcastMessage>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public BroadcastMessageConsumer(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<BroadcastMessage> context)
        {
            await _hubContext.Clients.All.SendAsync("broadcastMessage", context.Message.Name, context.Message.Message);
        }
    }
}
