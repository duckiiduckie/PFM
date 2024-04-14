namespace BudgetAPI.Services
{
    public interface IProduceMessage
    {
        Task ProduceMessageAsync(string userName);
    }
}
