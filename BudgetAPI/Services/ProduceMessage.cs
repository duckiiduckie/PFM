﻿
using MassTransit;
using Message;

namespace BudgetAPI.Services
{
    public class ProduceMessage : IProduceMessage
    {

        private readonly IPublishEndpoint _publishEndpoint;

        public ProduceMessage(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task ProduceMessageAsync(string userEmail)
        {
            var mess = new MessageTrans
            {
                Topic = "Budget",
                UserEmail = "duckie010203@gmail.com"
            };
            await _publishEndpoint.Publish(mess);
        }
    }
}
