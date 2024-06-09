namespace IncomeAPI.Services
{
    public interface IProduceMessage
    {
        Task ProduceMessageAsync(string userEmail);
    }
}
