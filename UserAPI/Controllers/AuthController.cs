using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserAPI.Models;
using UserAPI.Models.Dto;
using UserAPI.Sevices;

namespace UserAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IProduceMessage _produceMessage;
        private readonly UserManager<ApplicationUser> _userManager;
        protected ResponeDto _respone;

        public AuthController(IAuthService authService, IProduceMessage produceMessage, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _respone = new ResponeDto();
            _produceMessage = produceMessage;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _respone.Message = errorMessage;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
            await _produceMessage.ProduceMessageAsync(model.Email);
            return Ok(_respone);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginRespone = await _authService.Login(model);
            if (loginRespone.User == null)
            {
                _respone.Message = "Invalid Email or Password";
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
            _respone.Result = loginRespone;
            return Ok(_respone);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
                var user = await _userManager.FindByEmailAsync(model.Mail);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = WebUtility.UrlEncode(token);
                    var encodedEmail = WebUtility.UrlEncode(model.Mail);

                    var resetUrl = $"http://localhost:3000/reset-password?token={encodedToken}&email={encodedEmail}";
                    await _produceMessage.ProduceMessageForgot(model.Mail, $"Please reset your password by clicking here: <a href='{resetUrl}'>link</a>");
                }

                return Ok(new { Message = "If an account with that email exists, a password reset link has been sent." });

        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Invalid email or token.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            var result = await _authService.ChangePassword(model);
            if (result != "Succeeded")
            {
                _respone.Message = result;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
            return Ok(_respone);
        }
    }
}
