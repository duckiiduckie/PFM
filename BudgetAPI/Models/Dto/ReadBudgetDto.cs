using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models.Dto
{
    public class ReadBudgetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TargetAmount { get; set; }
        [NotMapped]
        public decimal UsedAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [NotMapped]
        public List<CategoryDto> Categories { get; set; }
        public string UserEmail { get; set; }
        public bool IsMailSent { get; set; }
    }
}
