using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TargetAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UsedAmount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
