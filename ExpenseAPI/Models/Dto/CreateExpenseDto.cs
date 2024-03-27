using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseAPI.Models.Dto
{
    public class CreateExpenseDto
    {
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string CategoryName { get; set; } = "";
        public string UserId { get; set; }
    }
}
