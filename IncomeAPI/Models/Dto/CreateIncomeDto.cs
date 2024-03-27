using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeAPI.Models.Dto
{
    public class CreateIncomeDto
    {
        public string UserId { get; set; }
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string CategoryName { get; set; } = "";
    }
}
