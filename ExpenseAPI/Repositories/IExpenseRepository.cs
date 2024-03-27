using ExpenseAPI.Models.Dto;

namespace ExpenseAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<ReadExpenseDto?>?> GetExpensesAsync(string userId);
        Task<ReadExpenseDto?> GetExpenseAsync(int id);
        Task<IEnumerable<ReadExpenseDto?>?> GetExpensesAsync(string userId, string type, int number);
        Task<ReadExpenseDto?> CreateExpenseAsync(CreateExpenseDto expense);
        Task<ReadExpenseDto?> UpdateExpenseAsync(int id,CreateExpenseDto expense);
        Task<ReadExpenseDto?> DeleteExpenseAsync(int id);
    }
}
