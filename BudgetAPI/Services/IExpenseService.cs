using BudgetAPI.Models.Dto;

namespace BudgetAPI.Services
{
    public interface IExpenseService
    {
        Task<List<ReadDailyExpense>> GetReadDailyExpensesAsync(string userId);
    }
}
