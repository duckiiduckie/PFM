using BudgetAPI.Models.Dto;

namespace BudgetAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ReadCategoryDto>> GetAllCategoryAsync();
        Task<ReadCategoryDto?> GetCategoryAsync(int CategoryId);
        Task<ReadCategoryDto?> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<ReadCategoryDto?> UpdateCategoryAsync(int id);
        Task<ReadCategoryDto?> DeleteCategoryAsync(int id);
    }
}
