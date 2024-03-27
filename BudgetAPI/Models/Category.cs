using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UsedAmount { get; set; }
        [Required]
        public int BudgetId { get; set; }
        [Required]
        [ForeignKey("BudgetId")]
        public Budget Budget { get; set; }
    }
}
