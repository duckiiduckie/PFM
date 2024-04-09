using BudgetAPI.Models.Dto;

namespace BudgetAPI.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ReadCategoryDto>?>? GetCategories(string userId);
    }
}
