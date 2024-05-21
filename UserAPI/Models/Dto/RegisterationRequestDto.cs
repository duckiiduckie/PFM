namespace UserAPI.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
