using System.ComponentModel.DataAnnotations;

namespace BudgetAPI.Models.Dto
{
    public class CreateBudgetDto
    {
        public string UserId { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserEmail { get; set; }
        public bool IsMailSent { get; set; }
    }
}
