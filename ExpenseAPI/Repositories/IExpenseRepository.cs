using ExpenseAPI.Models.Dto;

namespace ExpenseAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task<ReadFuturePlannedExpense> CreateFuturePlannedExpense(CreateFuturePlannedExpense createFuturePlannedExpense);
        Task<ReadDailyExpense> CreateDailyExpense(CreateDailyExpense createDailyExpense);
        Task<List<ReadFuturePlannedExpense?>?> GetFuturePlannedExpenses(string userId);
        Task<List<ReadDailyExpense?>?> GetDailyExpenses(string userId);
        Task<ReadFuturePlannedExpense?> GetFuturePlannedExpenseById(int id);
        Task<ReadDailyExpense?> GetDailyExpenseById(int id);
        Task<ReadFuturePlannedExpense> UpdateFuturePlannedExpense(int id, CreateFuturePlannedExpense createFuturePlannedExpense);
        Task<ReadDailyExpense> UpdateDailyExpense(int id, CreateDailyExpense createDailyExpense);
        Task<bool> DeleteFuturePlannedExpense(int id);
        Task<bool> DeleteDailyExpense(int id);
    }
}
