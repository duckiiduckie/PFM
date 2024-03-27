using System.ComponentModel.DataAnnotations;

namespace BudgetAPI.Models.Dto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal UsedAmount { get; set; }
        public int BudgetId { get; set; }
    }
}
