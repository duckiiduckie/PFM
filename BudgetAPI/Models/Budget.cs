using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TargetAmount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string UserEmail { get; set; }
        public bool IsMailSent { get; set; }
    }
}
