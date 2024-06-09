using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public bool IsMailSent { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Essential { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SavingAndInvestment { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Want { get; set; }
    }
}
