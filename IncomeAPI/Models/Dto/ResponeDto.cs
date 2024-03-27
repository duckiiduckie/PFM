namespace IncomeAPI.Models.Dto
{
    public class ResponeDto
    {
        public object? Result { get; set; } = null;
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
