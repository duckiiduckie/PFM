using Message;

namespace UserAPI.Sevices
{
    public interface IProduceMessage
    {
        Task ProduceMessageAsync(string userName);
    }
}
