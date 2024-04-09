
namespace BudgetAPI.Models.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; } = "";
        public decimal UsedAmount { get; set; }
        public List<ExpenseDto> Expenses { get; set; }
    }
}
