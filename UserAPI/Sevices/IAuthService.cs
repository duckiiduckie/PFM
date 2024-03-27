using UserAPI.Models.Dto;

namespace UserAPI.Sevices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponeDto> Login(LoginRequestDto loginRequestDto);
        Task<string> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
