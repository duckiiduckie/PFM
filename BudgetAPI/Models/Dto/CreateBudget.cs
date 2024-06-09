using System.ComponentModel.DataAnnotations;

namespace BudgetAPI.Models.Dto
{
    public class CreateBudget
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public string Type { get; set; }
        public bool IsMailSent { get; set; }
        public decimal Essential { get; set; }
        public decimal Want { get; set; }
        public decimal SavingAndInvestment { get; set; }
    }
}
