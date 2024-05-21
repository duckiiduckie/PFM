using System.Diagnostics.CodeAnalysis;

namespace BudgetAPI.Models.Dto
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; } = "";
    }
}
