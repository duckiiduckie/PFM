using System.ComponentModel.DataAnnotations;

namespace BudgetAPI.Models.Dto
{
    public class CreateBudgetDto
    {
        public decimal TargetAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
