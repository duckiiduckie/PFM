namespace UserAPI.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime BirthDay { get; set; }
    }
}
