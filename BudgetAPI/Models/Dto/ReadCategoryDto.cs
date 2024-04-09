
namespace BudgetAPI.Models.Dto
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; } = "";
        public List<ExpenseDto> Expenses { get; set; }
    }
}
