using BudgetAPI.Models.Dto;

namespace BudgetAPI.Repositories
{
    public interface IBudgetRepository
    {
        Task<List<ReadBudget>> GetAllBudgets();
        Task<ReadBudget> CreateBudget(CreateBudget createBudgetDto);
        Task<List<ReadBudget>> GetBudgets(string userId);
        Task<ReadBudget> GetBudgetById(int id);
        Task<ReadBudget> GetBudgetNow(string userId);
        Task<ReadBudget> UpdateBudget(int id, CreateBudget createBudgetDto);
        Task<bool> DeleteBudget(int id);
    }
}
