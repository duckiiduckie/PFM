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

        public async Task<IEnumerable<ReadCategoryDto>?>? GetCategories(string userId)
        {
            var client = _clientFactory.CreateClient("ExpenseAPI");
            var response = await client.GetAsync($"/api/category/getcategories/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponeDto>(content);
            if (res.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ReadCategoryDto>>(Convert.ToString(res.Result));
            }
            return new List<ReadCategoryDto>();
        }
    }
}
