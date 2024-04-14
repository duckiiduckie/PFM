using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        protected ResponeDto _respone;

        public AuthController(IAuthService authService, IProduceMessage produceMessage)
        {
            _authService = authService;
            _respone = new ResponeDto();
            _produceMessage = produceMessage;
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
