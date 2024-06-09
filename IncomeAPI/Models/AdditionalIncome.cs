using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeAPI.Models
{
    public class AdditionalIncome
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string Category { get; set; } = "";
    }
}
