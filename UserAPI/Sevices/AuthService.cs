using Microsoft.AspNetCore.Identity;
using UserAPI.DataAccess;
using UserAPI.Models;
using UserAPI.Models.Dto;

namespace UserAPI.Sevices
{
    public class AuthService:IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext context, UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = _context.ApplycationUsers.FirstOrDefault(u=> u.Id == changePasswordDto.UserId);
            var isValid = _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword).Result;
            if(isValid == false)
            {
                return "Invalid Password";
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return result.Succeeded ? "Succeeded" : result.Errors.First().Description;
        }

        public async Task<LoginResponeDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.ApplycationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Email.ToLower());
            bool isValid = _userManager.CheckPasswordAsync(user, loginRequestDto.Password).Result;
            if (user == null || isValid == false)
            {
                return new LoginResponeDto() { User = null, Token = null };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.UserName,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay
            };

            LoginResponeDto loginResponeDto = new LoginResponeDto
            {
                User = userDto,
                Token = token
            };
            return loginResponeDto;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                Email = registerationRequestDto.Email,
                PhoneNumber = "",
                FullName = registerationRequestDto.FullName,
                BirthDay = DateTime.Now
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplycationUsers.First(u => u.UserName == registerationRequestDto.Email);
                    var userDto = new UserDto
                    {
                        Id = userToReturn.Id,
                        FullName = userToReturn.FullName,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber,
                        BirthDay = userToReturn.BirthDay
                    };
                    return "";
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "Erro Encounted";
        }
    }
}
