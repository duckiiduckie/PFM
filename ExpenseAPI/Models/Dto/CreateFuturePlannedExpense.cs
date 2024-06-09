using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models.Dto
{
    public class CreateFuturePlannedExpense
    {
        public string UserId { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Status { get; set; } = ""; 
    }
}
