using System.Text.Json.Serialization;

namespace ExpenseAPI.Models.Dto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = "";
        public string UserId { get; set; }
    }
}
