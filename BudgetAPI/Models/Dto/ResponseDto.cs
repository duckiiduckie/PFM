namespace BudgetAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; } = null;
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
