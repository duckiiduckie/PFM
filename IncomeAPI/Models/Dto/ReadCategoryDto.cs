using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeAPI.Models.Dto
{
    public class ReadCategoryDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = "";
        [NotMapped]
        public IEnumerable<ReadIncomeDto> Incomes { get; set; }
    }
}
