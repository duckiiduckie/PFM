namespace UserAPI.Models.Dto
{
    public class ResponeDto
    {
        public object? Result { get; set; }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
    }
}
