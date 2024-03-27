using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models.Dto
{
    public class ReadBudgetDto
    {
        public int Id { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal UsedAmount { get; set; }
        [NotMapped]
        public IEnumerable<ReadCategoryDto> Categories { get; set; }
    }
}
