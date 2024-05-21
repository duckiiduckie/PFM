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

        public async Task ProduceMessageForgot(string userEmail,string message)
        {
            var mess = new MessageTrans
            {
                Topic = "Forgot",
                UserEmail = userEmail,
                Mess = message
            };
            await _publishEndpoint.Publish(mess);
        }
    }
}
