namespace ExpenseAPI.Models.Dto
{
    public class ReadExpenseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}
