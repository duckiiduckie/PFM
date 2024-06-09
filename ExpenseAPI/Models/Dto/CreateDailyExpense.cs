using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models.Dto
{
    public class CreateDailyExpense
    {
        public string UserId { get; set; } = "";
        public string Description { get; set; } = "";
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public string Type { get; set; }
    }
}
