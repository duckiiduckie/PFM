using MassTransit;
using Message;

namespace UserAPI.Sevices
{
    public class ProduceMessage: IProduceMessage
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
                Topic = "Register",
                UserEmail = userName
            };
            await _publishEndpoint.Publish(mess);
        }

    }
}
