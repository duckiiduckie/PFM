using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models.Dto
{
    public class ReadBudget
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public decimal UsedAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public string Type { get; set; }
        public bool IsMailSent { get; set; }
        public decimal Essential { get; set; }
        public decimal SavingAndInvestment { get; set; }
        public decimal Want { get; set; }
        [NotMapped]
        public decimal UsedEssential { get; set; }
        [NotMapped]
        public decimal UsedSavingAndInvestment { get; set; }
        [NotMapped]
        public decimal UsedWant { get; set; }
    }
}
