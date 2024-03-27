using System.ComponentModel.DataAnnotations;

namespace IncomeAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string UserId { get; set; }
    }
}
