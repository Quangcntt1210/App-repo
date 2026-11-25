using APP.DTOs;
using APP.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(
            [FromBody] LoginRequest request)
        {
            var result = _authService.Login(request);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Sai tài khoản hoặc mật khẩu"
                });
            }

            return Ok(result);
        }
    }
}