using IncomeAPI.Models.Dto;

namespace IncomeAPI.Repositories
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<ReadIncomeDto?>?> GetIncomesAsync(string userId);
        Task<ReadIncomeDto?> GetIncomeAsync(int id);
        Task<IEnumerable<ReadIncomeDto?>?> GetIncomesAsync(string userId,string type, int number);
        Task<ReadIncomeDto?> CreateIncomeAsync(CreateIncomeDto expense);
        Task<ReadIncomeDto?> UpdateIncomeAsync(int id,CreateIncomeDto expense);
        Task<ReadIncomeDto?> DeleteIncomeAsync(int id);
    }
}
