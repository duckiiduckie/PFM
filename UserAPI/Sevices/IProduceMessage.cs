using Message;

namespace UserAPI.Sevices
{
    public interface IProduceMessage
    {
        Task ProduceMessageAsync(string userName);
        Task ProduceMessageForgot(string userEmail, string message);
    }
}
