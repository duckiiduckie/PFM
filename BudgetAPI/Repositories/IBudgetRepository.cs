using BudgetAPI.Models.Dto;

namespace BudgetAPI.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<ReadBudgetDto>> GetAllBudgetAsync(string userId); 
        Task<ReadBudgetDto?> GetBudgetAsync(int budgetId);
        Task<ReadBudgetDto?> GetBudgetNowAsync(string userId);
        Task<ReadBudgetDto?> CreateBudgetAsync(CreateBudgetDto createBudgetDto);
        Task<ReadBudgetDto?> DeleteBudgetAsync(int id);
    }
}
