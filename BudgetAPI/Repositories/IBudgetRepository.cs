using BudgetAPI.Models.Dto;

namespace BudgetAPI.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<ReadBudgetDto>> GetAllBudgetAsync(); 
        Task<ReadBudgetDto?> GetBudgetAsync(int budgetId);

        Task<ReadBudgetDto?> CreateBudgetAsync(CreateBudgetDto createBudgetDto);
        Task<ReadBudgetDto?> UpdateBudgetAsyn(int id);
        Task<ReadBudgetDto?> DeleteBudgetAsync(int id);
    }
}
