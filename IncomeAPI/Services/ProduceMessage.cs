
using MassTransit;
using Message;

namespace IncomeAPI.Services
{
    public class ProduceMessage : IProduceMessage
    {

        private readonly IPublishEndpoint _publishEndpoint;

        public ProduceMessage(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task ProduceMessageAsync(string mess)
        {
            var mes = new MessageTrans
            {
                Topic = "Income",
                UserEmail = "duckie010203@gmail.com",
                Mess = "Den ngay nhan luong roi nhe!" + mess
            };
            await _publishEndpoint.Publish(mes);
        }
    }
}
