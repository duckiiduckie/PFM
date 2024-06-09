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
        public async Task<List<ReadDailyExpense>> GetReadDailyExpensesAsync(string userId)
        {
            var client = _clientFactory.CreateClient("ExpenseAPI");
            var response = await client.GetAsync($"/api/expense/read-daily-expenses/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (res.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<ReadDailyExpense>>(Convert.ToString(res.Result));
            }
            return new List<ReadDailyExpense>();
        }
    }
}
