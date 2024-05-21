using ExpenseAPI.Models.Dto;

namespace ExpenseAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ReadCategoryDto?>?> GetCategories(string userId);
        Task<ReadCategoryDto?> GetCategory(int id);
        Task<ReadCategoryDto?> GetCategory(string name,string userId);
        Task<ReadCategoryDto?> AddCategory(CreateCategoryDto category);
        Task<ReadCategoryDto?> UpdateCategory(int id, CreateCategoryDto category);
        Task<ReadCategoryDto?> DeleteCategory(int id);
    }
}
