using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models.Dto
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal UsedAmount { get; set; }
    }
}
