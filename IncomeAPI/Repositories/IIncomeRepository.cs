using IncomeAPI.Models.Dto;

namespace IncomeAPI.Repositories
{
    public interface IIncomeRepository
    {
        Task<ReadAdditionalIncome?> ReadAdditionalIncome(int id);
        Task<ReadMainIncome?> ReadMainIncome(int id);    
        Task<List<ReadAdditionalIncome?>?> ReadAdditionalIncomes(string userId);
        Task<List<ReadMainIncome?>?> ReadMainIncomes(string userId);
        Task<CreateAdditionalIncome> CreateAdditionalIncome(CreateAdditionalIncome createAdditionalIncome);
        Task<CreateMainIncome> CreateMainIncome(CreateMainIncome createMainIncome);
        Task<ReadAdditionalIncome> UpdateAdditionalIncome(int id, CreateAdditionalIncome createAdditionalIncome);
        Task<ReadMainIncome> UpdateMainIncome(int id, CreateMainIncome createMainIncome);
        Task<bool> DeleteAdditionalIncome(int id);
        Task<bool> DeleteMainIncome(int id);
        Task<List<ReadMainIncome>> GetAllMainIncome();
    }
}
