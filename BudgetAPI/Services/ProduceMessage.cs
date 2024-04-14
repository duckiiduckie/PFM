
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

        public async Task ProduceMessageAsync(string userName)
        {
            var mess = new MessageTrans
            {
                Topic = "Budget",
                UserEmail = userName
            };
            await _publishEndpoint.Publish(mess);
        }
    }
}
