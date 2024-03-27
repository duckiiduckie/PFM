using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseAPI.Models.Dto
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; } = "";
        [NotMapped]
        public IEnumerable<ReadExpenseDto> Expenses { get; set; }
    }
}
