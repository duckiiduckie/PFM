using BudgetAPI.Models.Dto;
using Newtonsoft.Json;

namespace BudgetAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IHttpClientFactory _clientFactory;
        public ExpenseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }   
        public async Task<IEnumerable<ExpenseDto?>?> GetAllExpenses()
        {
            var client = _clientFactory.CreateClient("ExpenseAPI");
            var response = await client.GetAsync($"api/expense");
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponeDto>(content);
            if (res.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ExpenseDto>>(Convert.ToString(res.Result));
            }
            return new List<ExpenseDto>();
        }
    }
}
